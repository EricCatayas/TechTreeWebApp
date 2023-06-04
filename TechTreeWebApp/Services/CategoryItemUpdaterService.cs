using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class CategoryItemUpdaterService : ICategoryItemUpdaterService
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemUpdaterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task UpdateCategoryItem(CategoryItem categoryItem)
        {
            CategoryItem? categoryItem_ToUpdate = _context.CategoryItem.FirstOrDefault(temp=> temp.Id == categoryItem.Id);
            if (categoryItem_ToUpdate != null) 
            {
                categoryItem_ToUpdate.Title = categoryItem.Title;
                categoryItem_ToUpdate.Description = categoryItem.Description;
                categoryItem_ToUpdate.DateItemReleased = categoryItem.DateItemReleased;
                categoryItem_ToUpdate.MediaTypeId= categoryItem.MediaTypeId;
                _context.Update(categoryItem_ToUpdate);
                await _context.SaveChangesAsync();
            }
        }
    }
}
