using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using System.Diagnostics;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Controllers
{
    /// <summary>
    /// When the user clicks on a CategoryTitle, the user is directed to a razor view of the relevant items
    /// </summary>
    public class ContentController : Controller
    {
        private readonly IContentGetterService _contentService;
        public ContentController(IContentGetterService contentService)
        {
            _contentService = contentService;
        }
        public async Task<IActionResult> Index(int categoryItemId)
        {
            Content content = await _contentService.GetContentByCategoryItemID(categoryItemId);
            return View(content); 
        }
    }
}
