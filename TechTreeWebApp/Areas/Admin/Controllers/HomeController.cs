using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TechTreeWebApp.Data;
using TechTreeWebApp.Extentions;
using TechTreeWebApp.Models;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ICategoryItemDetailsGetterService _categoryItemDetailsGetterService;

        public HomeController(ICategoryItemDetailsGetterService categoryItemDetailsGetterService)
        {
            _categoryItemDetailsGetterService = categoryItemDetailsGetterService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryItemDetailsModel> categoryItemDetailsModels = await _categoryItemDetailsGetterService.GetAllCategoryItemDetails();
            IEnumerable<GroupedCategoryItemsByCategoryModel> groupedCategoryItemsByCategoryModels = categoryItemDetailsModels.GetGroupedCategoryItemsByCategoryModels();

            CategoryDetailsModel categoryDetailsModel = new()
            {
                GroupedCategoryItemsByCategoryModels = groupedCategoryItemsByCategoryModels
            };
            return View(categoryDetailsModel);
        }
    }
}
