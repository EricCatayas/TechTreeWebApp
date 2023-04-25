using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.Models;

namespace TechTreeWebApp.Controllers
{
    [Authorize]
    public class CategoriesToUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDataFunctions _dataFunctions;

        public CategoriesToUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IDataFunctions dataFunctions)
        {
            _context = context;
            _userManager = userManager;
            _dataFunctions = dataFunctions;
        }
        public async Task<IActionResult> Index() //check
        {
            CategoriesToUserModel categoriesToUserModel = new();
            string userId = _userManager.GetUserAsync(User).Result?.Id;
            if (userId != null) 
            {
                categoriesToUserModel.SelectedCategories = await GetCategoriesCurrentlySavedForUser(userId);     
                categoriesToUserModel.Categories = await GetCategoriesThatHaveContent();
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

            List<UserCategory> categoriesForUserToAdd = GetCategoriesToAddForUser(categoriesSelected, userId);
            List<UserCategory> categoriesForUserToDelete = GetCategoriesToDeleteForUser(userId);

            await _dataFunctions.UpdataUserCategoryEntityAsync(categoriesForUserToAdd, categoriesForUserToDelete);
            return RedirectToAction("Index", "Home");
        }
   
        private List<UserCategory> GetCategoriesToAddForUser(string[] categoryIds, string userId) 
        {
            var categoriesToAdd = (from categoryId in categoryIds
                                   select new UserCategory
                                   {
                                       UserId = userId,
                                       CategoryId = int.Parse(categoryId),
                                   }).ToList();
            return categoriesToAdd;

        }
        private async Task<List<Category>> GetCategoriesThatHaveContent()
        {
            var allCategories = await (from category in _context.Category 
                                       join categoryItem in _context.CategoryItem
                                       on category.Id equals categoryItem.CategoryId
                                       join content in _context.Content
                                       on categoryItem.Id equals content.CategoryItem.Id
                                       select new Category
                                       {
                                           Id = category.Id,
                                           Title = category.Title,
                                           ThumbnailImagePath = category.ThumbnailImagePath,
                                           Description= category.Description                                           
                                       }).Distinct().ToListAsync();
            return allCategories;
        }
        private async Task<List<Category>> GetCategoriesCurrentlySavedForUser(string userId) 
        {
            var categoriesCurrentlySavedByUser = await (from userCategory in _context.UserCategory 
                                                        where userCategory.UserId == userId
                                                        select new Category
                                                        {
                                                            Id = userCategory.CategoryId
                                                        }).ToListAsync();
            return categoriesCurrentlySavedByUser;
        }
        private List<UserCategory> GetCategoriesToDeleteForUser(string userId) 
        {
            var categoriesForDeletion = (from userCategory in _context.UserCategory
                                         where userCategory.UserId == userId
                                         select new UserCategory
                                         {
                                            Id = userCategory.Id,
                                            UserId = userCategory.UserId,
                                            CategoryId= userCategory.CategoryId,
                                         }).ToList();
            return categoriesForDeletion;
        }
    

    }
}
