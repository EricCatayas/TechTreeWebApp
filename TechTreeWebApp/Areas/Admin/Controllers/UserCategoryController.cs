using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Areas.Admin.Models;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;

namespace TechTreeWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserCategoryController : Controller
    {
        private readonly ApplicationDbContext _context; //passed in via Dependency Injection
        private IDataFunctions _IDataFunctions;

        public UserCategoryController(ApplicationDbContext context, IDataFunctions iDataFunctions) 
        {
            _context = context;
            _IDataFunctions = iDataFunctions;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync()); // @model IEnumerable<Category> in Index
        }
        [HttpGet] // type "GET" in UsersToCategory.js
        public async Task<IActionResult> GetUsersForCategory(int categoryId)
        {            
            var allUsers = await GetAllUsers();
            var allSavedSelectedUsers = await GetSavedSelectedUsersForCategory(categoryId); //check

            UsersCategoryListModel usersCategoryListModel = new()
            {
                UsersCollection = allUsers,
                UsersSelected = allSavedSelectedUsers,   
            };
            return PartialView("_UsersListViewPartial",usersCategoryListModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSelectedUsers([Bind("CategoryId , UsersSelected")] UsersCategoryListModel usersCategoryListModel) // Refer: UsersToCategory.js and Index.cshtml
        {
            List<UserCategory> usersSelectedForCategoryToAdd = new();
            var usersSelectedForCategoryToDel = await GetUsersForCategoryToDelete(usersCategoryListModel.CategoryId);

            if (usersCategoryListModel.UsersSelected != null) //<-- Check if there are selected checkboxes 
            {
                usersSelectedForCategoryToAdd = await GetUsersForCategoryToAdd(usersCategoryListModel);
            }
                await _IDataFunctions.UpdataUserCategoryEntityAsync(usersSelectedForCategoryToAdd, usersSelectedForCategoryToDel);
            
            usersCategoryListModel.UsersCollection = await GetAllUsers();
            return PartialView("_UsersListViewPartial", usersCategoryListModel); // N: model="new UsersCategoryListModel{}" in Index.cshtml
        }
        private async Task<List<UserCategory>> GetUsersForCategoryToAdd(UsersCategoryListModel usersCategoryListModel) 
        {
            var usersForCategoryToAdd = (from userModel in usersCategoryListModel.UsersSelected // Mistake: UsersCollection
                                         select new UserCategory
                                         {
                                             CategoryId = usersCategoryListModel.CategoryId,
                                             UserId= userModel.Id,
                                         }).ToList();
            return await Task.FromResult(usersForCategoryToAdd);
        }
        private async Task<List<UserCategory>> GetUsersForCategoryToDelete(int categoryId) 
        {
            var usersToDelete = (from userCat in _context.UserCategory
                                 where userCat.CategoryId == categoryId
                                 select new UserCategory
                                 {
                                     CategoryId = categoryId,
                                     Id = userCat.Id,
                                     UserId = userCat.UserId
                                 }).ToListAsync();

            return await usersToDelete; 
        }
        private async Task<List<UserModel>> GetAllUsers()
        {
            var allUserModel = (from user in _context.Users
                            select new UserModel
                            {
                                Id = user.Id, 
                                UserName = user.UserName,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                            }).ToListAsync();
            return await allUserModel;
        }
        private async Task<List<UserModel>> GetSavedSelectedUsersForCategory(int categoryId)
        {
            var allSelectedUserModel = (from userToCat in _context.UserCategory
                                        where userToCat.CategoryId == categoryId
                                        select new UserModel
                                        {
                                            Id = userToCat.UserId
                                        }).ToListAsync();
            return await allSelectedUserModel;
        }
    }
}
