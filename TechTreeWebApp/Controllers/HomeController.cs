using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechTreeWebApp.Data;
using TechTreeWebApp.Extentions;
using TechTreeWebApp.Filters;
using TechTreeWebApp.Models;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryItemDetailsGetterService _categoryItemDetailsGetterService;
        private readonly ICategoriesToUserGetterServices _categoriesGetterServices;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ICategoryItemDetailsGetterService categoryItemDetailsGetterService, ICategoriesToUserGetterServices categoriesGetterServices,   SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IEmailService emailService)
        { // passing via Dependency Injection
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _categoryItemDetailsGetterService = categoryItemDetailsGetterService;
            _categoriesGetterServices = categoriesGetterServices;
            _emailService = emailService;
        }
        [TypeFilter(typeof(AdminAreaAuthorizationFilter))]
        public async Task<IActionResult> Index()
        {
            CategoryDetailsModel categoryDetailsModel = new();
            if (_signInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(User);
                if (userId != null)
                {
                    IEnumerable<CategoryItemDetailsModel> categoryItemDetailsModels = await _categoryItemDetailsGetterService.GetCategoryItemDetailsForUser(userId);
                    IEnumerable<GroupedCategoryItemsByCategoryModel> groupedCategoryItemsByCategoryModels = categoryItemDetailsModels.GetGroupedCategoryItemsByCategoryModels();

                    categoryDetailsModel.GroupedCategoryItemsByCategoryModels = groupedCategoryItemsByCategoryModels;
                    return View(categoryDetailsModel);
                }                
            }
            else
            {
                categoryDetailsModel.Categories = await _categoriesGetterServices.GetCategoriesThatHaveContent();
            }
            
            return View(categoryDetailsModel);
        }
        [Route("[controller]/[action]")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmail(string recipientEmail, string name, string body)
        {
            bool success = await _emailService.SendEmail(recipientEmail, name, body);
            return success ? StatusCode(200) : StatusCode(500);
        }
        public IActionResult Privacy() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}