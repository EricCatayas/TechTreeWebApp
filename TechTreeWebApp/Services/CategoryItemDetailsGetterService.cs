using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Models;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class CategoryItemDetailsGetterService : ICategoryItemDetailsGetterService
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemDetailsGetterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CategoryItemDetailsModel>> GetAllCategoryItemDetails()
        {
            return await(from catItem in _context.CategoryItem
                         join category in _context.Category
                         on catItem.CategoryId equals category.Id
                         join content in _context.Content
                         on catItem.Id equals content.CategoryItemId 
                         /*join userCat in _context.UserCategory
                         on category.Id equals userCat.CategoryId*/
                         join mediaType in _context.MediaType
                         on catItem.MediaTypeId equals mediaType.Id
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

        public async Task<IEnumerable<CategoryItemDetailsModel>> GetCategoryItemDetailsForUser(string userId)
        {
            return await(from catItem in _context.CategoryItem
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
    }
}
