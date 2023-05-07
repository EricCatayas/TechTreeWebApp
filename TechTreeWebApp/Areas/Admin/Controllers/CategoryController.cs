using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoriesAdderService _categoriesAdderService;
        private readonly ICategoriesGetterService _categoriesGetterService;
        private readonly ICategoriesDeleterService _categoriesDeleterService;
        private readonly ICategoriesUpdaterService _categoriesUpdaterService;

        public CategoryController(ApplicationDbContext context, ICategoriesAdderService categoriesAdderService, ICategoriesGetterService categoriesGetterService, ICategoriesDeleterService categoriesDeleterService, ICategoriesUpdaterService categoriesUpdaterService)
        {
            _categoriesAdderService = categoriesAdderService;
            _categoriesGetterService = categoriesGetterService;
            _categoriesDeleterService = categoriesDeleterService;
            _categoriesUpdaterService = categoriesUpdaterService;
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index()
        {
              return View(await _categoriesGetterService.GetAllCategories());
        }

        // GET: Admin/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoriesGetterService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ThumbnailImagePath")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoriesAdderService.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoriesGetterService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ThumbnailImagePath")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoriesUpdaterService.UpdateCategory(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoriesGetterService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoriesGetterService.GetCategoryById(id);

            if (category != null)
            {
                await _categoriesDeleterService.DeleteCategory(category);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
