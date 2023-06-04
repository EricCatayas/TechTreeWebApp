using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.Extentions;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] //Prevents unauthorized users from entering 
    public class CategoryItemController : Controller
    {
        private readonly ICategoryItemAdderService _categoryItemAdderService;
        private readonly ICategoryItemGetterService _categoryItemGetterService;
        private readonly ICategoryItemDeleterService _categoryItemDeleterService;
        private readonly ICategoryItemUpdaterService _categoryItemUpdaterService;
        private readonly IMediaTypeGetterService _mediaTypeGetterService;

        public CategoryItemController(ICategoryItemAdderService categoryItemAdderService, ICategoryItemGetterService categoryItemGetterService, ICategoryItemDeleterService categoryItemDeleterService, ICategoryItemUpdaterService categoryItemUpdaterService, IMediaTypeGetterService mediaTypeGetterService)
        {
            _categoryItemAdderService = categoryItemAdderService;
            _categoryItemGetterService = categoryItemGetterService;
            _mediaTypeGetterService = mediaTypeGetterService;
            _categoryItemDeleterService = categoryItemDeleterService;
            _categoryItemUpdaterService = categoryItemUpdaterService;
        }

        // GET: Admin/CategoryItem
        public async Task<IActionResult> Index(int categoryId)
        {
            // Ex: Category "C# for beginners,  Categoryitem "data variables, methods, etc"
            List<CategoryItem>? categoryItems = await _categoryItemGetterService.GetCategoryItemsByCategoryId(categoryId);
            ViewBag.CategoryId = categoryId;
              return View(categoryItems);
        }

        // GET: Admin/CategoryItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _categoryItemGetterService.GetCategoryItemById(id);
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
            List<MediaType>? mediaTypes = await _mediaTypeGetterService.GetMediaTypes();
            CategoryItem categoryItem = new CategoryItem
            {
                CategoryId = categoryId,
                MediaTypes = mediaTypes?.ConvertToSelectList(0) //by default first elem is displayed
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
                await _categoryItemAdderService.AddCategoryItem(categoryItem);
                return RedirectToAction(nameof(Index), new { categoryId = categoryItem.CategoryId }); //Forgor this?
            }
            List<MediaType>? mediaTypes = await _mediaTypeGetterService.GetMediaTypes(); //The user can reselect the Default item in dropdown list -- The videoItem and article must be restricted , when the Create view is displayed
            categoryItem.MediaTypes = mediaTypes?.ConvertToSelectList(categoryItem.MediaTypeId);

            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<MediaType>? mediaTypes = await _mediaTypeGetterService.GetMediaTypes();
            var categoryItem = await _categoryItemGetterService.GetCategoryItemById(id); // finds the relevant categoryItem data from db
            if (categoryItem == null)
            {
                return NotFound();
            }
            categoryItem.MediaTypes = mediaTypes?.ConvertToSelectList(categoryItem.MediaTypeId);
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
                    await _categoryItemUpdaterService.UpdateCategoryItem(categoryItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index), new {categoryId = categoryItem.CategoryId}); // when redirected to Index.cshtml, the list of categoryItems related to the Category will be presented
            }
            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _categoryItemGetterService.GetCategoryItemById(id);       
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
            var categoryItem = await _categoryItemGetterService.GetCategoryItemById(id);
            if (categoryItem != null)
            {
                await _categoryItemDeleterService.DeleteCategoryItem(categoryItem);
            }
            return RedirectToAction(nameof(Index), new {categoryId = categoryItem.CategoryId});
        }
    }
}
