using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using System.Diagnostics;

namespace TechTreeWebApp.Controllers
{
    /// <summary>
    /// When the user clicks on a CategoryTitle, the user is directed to a razor view of the relevant items
    /// </summary>
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int categoryItemId)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            //var content = await _context.Content.SingleOrDefaultAsync(item => item.CategoryItem.Id == categoryItemId).
            Content content = await (from _content in _context.Content
                                     where _content.CategoryItem.Id == categoryItemId //Mistake: _content.CatItemId when the specified member is unmapped
                                     select new Content
                                     {
                                         Title = _content.Title,
                                         VideoLink = _content.VideoLink,
                                         HTMLContent = _content.HTMLContent,
                                     }).FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return View(content); //// Because of naming convention inherent in the MVC framework; MVC knows this Controller invokes Content.Index.cshtml in Views despite being defined separately from the Razor view
        }
    }
}
