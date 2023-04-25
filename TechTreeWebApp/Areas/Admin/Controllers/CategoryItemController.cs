using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.Extentions;

namespace TechTreeWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] //Prevents unauthorized users from entering 
    public class CategoryItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CategoryItem
        public async Task<IActionResult> Index(int categoryId)
        {
            // Ex: Category "C# for beginners,  Categoryitem "data variables, methods, etc"
            List<CategoryItem> categoryItems = await (from CatItem in _context.CategoryItem
                                                      join contentItem in _context.Content    
                                                      on CatItem.Id equals contentItem.CategoryItem.Id   // CAtegoryItem "Foreign Key" in Content
                                                      into gj                                            // Then C: a group for the contentItems related to the CategoryItem
                                                      from subContent in gj.DefaultIfEmpty()             // Select into an obj "subContent" -- set to null if CategoryItem does not content
                                                      where CatItem.CategoryId == categoryId
                                                      select new CategoryItem
                                                      {
                                                          Id = CatItem.Id,
                                                          CategoryId = categoryId,
                                                          Title = CatItem.Title,
                                                          Description = CatItem.Description,
                                                          MediaTypeId = CatItem.MediaTypeId,
                                                          DateItemReleased = CatItem.DateItemReleased,
                                                          ContentId = (subContent != null)? subContent.Id : 0 // if !null, navigate to Edit() else navigate to Create() in ContentControll
                                                      }).ToListAsync();
            ViewBag.CategoryId = categoryId;
              return View(categoryItems);
        }

        // GET: Admin/CategoryItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryItem == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Create
        // This Create() contains html form which the administrator can use to add value to relevant form fields that will be submitted to
        //      the relevant Action Method Field contained within the CAtegoryItem controll class, then saved to the db
        public async Task<IActionResult> Create(int categoryId)
        {
            List<MediaType> mediaTypes = await _context.MediaType.ToListAsync();
            CategoryItem categoryItem = new CategoryItem
            {
                CategoryId = categoryId,
                MediaTypes = mediaTypes.ConvertToSelectList(0) //by default first elem is displayed
            };
            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] //This Create() processes data submitted to the server from the client via http post request
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,CategoryId,Description,MediaTypeId,DateItemReleased")] CategoryItem categoryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { categoryId = categoryItem.CategoryId }); //Forgor this?
            }
            List<MediaType> mediaTypes = await _context.MediaType.ToListAsync(); //The user can reselect the Default item in dropdown list -- The videoItem and article must be restricted , when the Create view is displayed
            categoryItem.MediaTypes = mediaTypes.ConvertToSelectList(categoryItem.MediaTypeId);

            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryItem == null)
            {
                return NotFound();
            }
            List<MediaType> mediaTypes = await _context.MediaType.ToListAsync();
            var categoryItem = await _context.CategoryItem.FindAsync(id); // finds the relevant categoryItem data from db
            if (categoryItem == null)
            {
                return NotFound();
            }
            categoryItem.MediaTypes = mediaTypes.ConvertToSelectList(categoryItem.MediaTypeId);
            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CategoryId,Description,MediaTypeId,DateItemReleased")] CategoryItem categoryItem)
        {
            if (id != categoryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryItemExists(categoryItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {categoryId = categoryItem.CategoryId}); // when redirected to Index.cshtml, the list of categoryItems related to the Category will be presented
            }
            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryItem == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItem.FirstOrDefaultAsync(m => m.Id == id);            
            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CategoryItem'  is null.");
            }
            var categoryItem = await _context.CategoryItem.FindAsync(id);
            if (categoryItem != null)
            {
                ContentController.DeleteCall(categoryItem.CategoryId, _context);
                _context.CategoryItem.Remove(categoryItem);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {categoryId = categoryItem.CategoryId});
        }

        public static void DeleteCall(int categoryId, ApplicationDbContext _context) // Call fOR CategoryController
        {
            foreach(CategoryItem categoryItem in _context.CategoryItem)
            {
                if (categoryItem.CategoryId == categoryId)
                    _context.CategoryItem.Remove(categoryItem);
            }
            ContentController.DeleteCall(categoryId, _context);

        }

        private bool CategoryItemExists(int id)
        {
          return _context.CategoryItem.Any(e => e.Id == id);
        }
    }
}
