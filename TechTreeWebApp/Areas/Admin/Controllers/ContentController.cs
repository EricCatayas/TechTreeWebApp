using System;
using System.Collections.Generic;
using System.Data;
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
    public class ContentController : Controller
    {
        private readonly ICategoryItemGetterService _categoryItemGetterService;
        private readonly IContentAdderService _contentAdderService;
        private readonly IContentGetterService _contentGetterService;
        private readonly IContentUpdaterService _contentUpdaterService;

        public ContentController(IContentAdderService contentAdderService, IContentGetterService contentGetterService, IContentUpdaterService contentUpdaterService, ICategoryItemGetterService categoryItemGetterService)
        {
            _contentAdderService = contentAdderService;
            _contentGetterService = contentGetterService;
            _contentUpdaterService = contentUpdaterService;
            _categoryItemGetterService = categoryItemGetterService;
        }
        public async Task<IActionResult> Index(int categoryItemId)
        {
            var content = await _contentGetterService.GetContentByCategoryItemID(categoryItemId);
            return View(content);
        }
        // GET: Admin/Content/Create
        public IActionResult Create(int categoryItemId, int categoryId)
        {
            Content content = new Content
            {
                CategoryItemId = categoryItemId,
                CategoryId = categoryId
            };

            return View(content);
        }

        // POST: Admin/Content/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HTMLContent,VideoLink, CategoryItemId, CategoryId")] Content content)
        {
            if (ModelState.IsValid)
            {
                content.CategoryItem = await _categoryItemGetterService.GetCategoryItemById(content.CategoryItemId);
                await _contentAdderService.AddContent(content);
                return RedirectToAction(nameof(Index), "CategoryItem", new { categoryId = content.CategoryId }); //(action, controller, value)
            }
            return View(content);
        }

        // GET: Admin/Content/Edit/5
        public async Task<IActionResult> Edit(int categoryItemId, int categoryId)
        {
            if (categoryItemId == 0)
            {
                return NotFound();
            }
            var content = await _contentGetterService.GetContentByCategoryItemID(categoryItemId);
            content.CategoryId = categoryId;
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Admin/Content/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HTMLContent,VideoLink,CategoryId")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _contentUpdaterService.UpdateContent(content);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingContent = await _contentGetterService.GetContentByID(content.Id);
                    if (existingContent == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "CategoryItem", new { categoryId = content.CategoryId }); /// TOO
            }
            return View(content);
        }
    }
}
