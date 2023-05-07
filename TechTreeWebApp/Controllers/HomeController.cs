﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.Models;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ICategoriesToUserGetterServices _categoriesGetterServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, ICategoriesToUserGetterServices categoriesGetterServices,  SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        { // passing via Dependency Injection
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _categoriesGetterServices = categoriesGetterServices;
        }

        public async Task<IActionResult> Index()
        {
            CategoryDetailsModel categoryDetailsModel = new();
            if (_signInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(User);
                if (userId != null)
                {
                    IEnumerable<CategoryItemDetailsModel> categoryItemDetailsModels = await GetCategoryItemDetailsForUser(userId);
                    IEnumerable<GroupedCategoryItemsByCategoryModel> groupedCategoryItemsByCategoryModels = GetGroupedCategoryItemsByCategoryModels(categoryItemDetailsModels);

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
        private async Task<IEnumerable<CategoryItemDetailsModel>> GetCategoryItemDetailsForUser(string userId)
        {
            return await (from catItem in _context.CategoryItem
                          join category in _context.Category
                          on catItem.CategoryId equals category.Id
                          join content in _context.Content
                          on catItem.Id equals content.CategoryItem.Id
                          join userCat in _context.UserCategory
                          on category.Id equals userCat.CategoryId
                          join mediaType in _context.MediaType
                          on catItem.MediaTypeId equals mediaType.Id
                          where userCat.UserId == userId         
                          select new CategoryItemDetailsModel
                          {
                              CategoryId = category.Id,
                              CategoryTitle = category.Title,
                              CategoryItemId = catItem.Id,
                              CategoryItemTitle = catItem.Title,
                              CategoryItemDescription = catItem.Description,
                              MediaImagePath = mediaType.ThumbnailImagePath
                          }).ToListAsync();
        }
        private IEnumerable<GroupedCategoryItemsByCategoryModel> GetGroupedCategoryItemsByCategoryModels(IEnumerable<CategoryItemDetailsModel> categoryItemDetailsModel)
        {
            return (from item in categoryItemDetailsModel
                    group item by item.CategoryId into g
                    select new GroupedCategoryItemsByCategoryModel
                    {
                        Id = g.Key,
                        Title = g.Select(c => c.CategoryTitle).FirstOrDefault(),
                        Items = g
                    });

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