using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.Models;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Controllers
{
    [Authorize]
    public class CategoriesToUserController : Controller
    {
        private readonly ICategoriesToUserGetterServices _categoriesGetterServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDataFunctions _dataFunctions;

        public CategoriesToUserController(UserManager<ApplicationUser> userManager, ICategoriesToUserGetterServices categoriesServices, IDataFunctions dataFunctions)
        {
            _userManager = userManager;
            _categoriesGetterServices = categoriesServices;
            _dataFunctions = dataFunctions;
        }
        public async Task<IActionResult> Index() 
        {
            CategoriesToUserModel categoriesToUserModel = new();
            string userId = _userManager.GetUserAsync(User).Result?.Id;
            if (userId != null) 
            {
                categoriesToUserModel.SelectedCategories = await _categoriesGetterServices.GetCategoriesCurrentlySavedForUser(userId);     
                categoriesToUserModel.Categories = await _categoriesGetterServices.GetCategoriesThatHaveContent();
                categoriesToUserModel.UserId = userId;
            }

            return View(categoriesToUserModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string[] categoriesSelected) 
        {
            string userId = _userManager.GetUserAsync(User).Result?.Id;
            CategoriesToUserModel categoriesToUserModel = new();

            List<UserCategory> categoriesForUserToAdd = _categoriesGetterServices.GetCategoriesToAddForUser(categoriesSelected, userId);
            List<UserCategory> categoriesForUserToDelete = _categoriesGetterServices.GetCategoriesToDeleteForUser(userId);

            await _dataFunctions.UpdataUserCategoryEntityAsync(categoriesForUserToAdd, categoriesForUserToDelete);
            return RedirectToAction("Index", "Home");
        }
    }
}
