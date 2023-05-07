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
    public class MediaTypeController : Controller
    {
        private readonly IMediaTypeAdderService _mediaTypeAdderService;
        private readonly IMediaTypeGetterService _mediaTypeGetterService;
        private readonly IMediaTypeDeleterService _mediaTypeDeleterService;
        private readonly IMediaTypeUpdaterService _mediaTypeUpdaterService;

        public MediaTypeController(IMediaTypeAdderService mediaTypeAdderService, IMediaTypeGetterService mediaTypeGetterService, IMediaTypeDeleterService mediaTypeDeleterService, IMediaTypeUpdaterService mediaTypeUpdaterService)
        {
            _mediaTypeAdderService = mediaTypeAdderService;
            _mediaTypeGetterService = mediaTypeGetterService;
            _mediaTypeDeleterService = mediaTypeDeleterService;
            _mediaTypeUpdaterService = mediaTypeUpdaterService;
        }

        // GET: Admin/MediaType
        public async Task<IActionResult> Index()
        {
              return View(await _mediaTypeGetterService.GetMediaTypes());
        }

        // GET: Admin/MediaType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _mediaTypeGetterService.GetMediaTypeById(id);
            if (mediaType == null)
            {
                return NotFound();
            }

            return View(mediaType);
        }

        // GET: Admin/MediaType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MediaType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ThumbnailImagePath")] MediaType mediaType)
        {
            if (ModelState.IsValid)
            {
                await _mediaTypeAdderService.AddMediaType(mediaType);
                return RedirectToAction(nameof(Index));
            }
            return View(mediaType);
        }

        // GET: Admin/MediaType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _mediaTypeGetterService.GetMediaTypeById(id);
            if (mediaType == null)
            {
                return NotFound();
            }
            return View(mediaType);
        }

        // POST: Admin/MediaType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ThumbnailImagePath")] MediaType mediaType)
        {
            if (id != mediaType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _mediaTypeUpdaterService.UpdateMediaType(mediaType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existingMediaType = await _mediaTypeGetterService.GetMediaTypeById(mediaType.Id);
                    if (existingMediaType == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mediaType);
        }

        // GET: Admin/MediaType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _mediaTypeGetterService.GetMediaTypeById(id);
            if (mediaType == null)
            {
                return NotFound();
            }

            return View(mediaType);
        }

        // POST: Admin/MediaType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaType = await _mediaTypeGetterService.GetMediaTypeById(id);
            if (mediaType != null)
            {
                await _mediaTypeDeleterService.DeleteMediaType(mediaType);
            }
            
            return RedirectToAction(nameof(Index));
        }

    }
}
