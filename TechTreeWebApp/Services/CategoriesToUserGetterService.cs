using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class CategoriesToUserGetterService : ICategoriesToUserGetterServices
    {
        private readonly ApplicationDbContext _context;
        public CategoriesToUserGetterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategoriesCurrentlySavedForUser(string userId)
        {
            var categoriesCurrentlySavedByUser = await(from userCategory in _context.UserCategory
                                                       where userCategory.UserId == userId
                                                       select new Category
                                                       {
                                                           Id = userCategory.CategoryId
                                                       }).ToListAsync();
            return categoriesCurrentlySavedByUser;
        }

        public async Task<List<Category>> GetCategoriesThatHaveContent()
        {
            var allCategories = await(from category in _context.Category
                                      join categoryItem in _context.CategoryItem
                                      on category.Id equals categoryItem.CategoryId
                                      join content in _context.Content
                                      on categoryItem.Id equals content.CategoryItem.Id
                                      select new Category
                                      {
                                          Id = category.Id,
                                          Title = category.Title,
                                          ThumbnailImagePath = category.ThumbnailImagePath,
                                          Description = category.Description
                                      }).Distinct().ToListAsync();
            return allCategories;
        }

        public List<UserCategory> GetCategoriesToAddForUser(string[] categoryIds, string userId)
        {
            var categoriesToAdd = (from categoryId in categoryIds
                                   select new UserCategory
                                   {
                                       UserId = userId,
                                       CategoryId = int.Parse(categoryId),
                                   }).ToList();
            return categoriesToAdd;
        }

        public List<UserCategory> GetCategoriesToDeleteForUser(string userId)
        {
            var categoriesForDeletion = (from userCategory in _context.UserCategory
                                         where userCategory.UserId == userId
                                         select new UserCategory
                                         {
                                             Id = userCategory.Id,
                                             UserId = userCategory.UserId,
                                             CategoryId = userCategory.CategoryId,
                                         }).ToList();
            return categoriesForDeletion;
        }
    }
}
