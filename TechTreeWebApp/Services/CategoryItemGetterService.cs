using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class CategoryItemGetterService : ICategoryItemGetterService
    {
        private readonly ApplicationDbContext _context;
        public CategoryItemGetterService(ApplicationDbContext context)
        {
            _context= context;
        }

        public async Task<List<CategoryItem>?> GetAllCategoryItems()
        {
            return await _context.CategoryItem.ToListAsync();
        }

        public async Task<List<CategoryItem>?> GetCategoryItemByCategoryId(int? categoryId)
        {
            if (categoryId == null)
                return null;
            return await(from CatItem in _context.CategoryItem
                        join contentItem in _context.Content
                        on CatItem.Id equals contentItem.CategoryItem.Id   // CAtegoryItem "Foreign Key" in Content
                        into gj                                            // Then C: a group for the contentItems related to the CategoryItem
                        from subContent in gj.DefaultIfEmpty()             // Select into an obj "subContent" -- set to null if CategoryItem does not content
                        where CatItem.CategoryId == categoryId
                        select new CategoryItem
                        {
                            Id = CatItem.Id,
                            CategoryId = (int)categoryId,
                            Title = CatItem.Title,
                            Description = CatItem.Description,
                            MediaTypeId = CatItem.MediaTypeId,
                            DateItemReleased = CatItem.DateItemReleased,
                            ContentId = (subContent != null) ? subContent.Id : 0 // if !null, navigate to Edit() else navigate to Create() in ContentControll
                        }).ToListAsync();
        }

        public async Task<CategoryItem?> GetCategoryItemById(int catItemId)
        {
           return await _context.CategoryItem.FindAsync(catItemId);
        }

        public Task<CategoryItem?> GetCategoryItemById(int? catItemId)
        {
            return _context.CategoryItem.FirstOrDefaultAsync(item => item.Id == catItemId);
        }
    }
}
