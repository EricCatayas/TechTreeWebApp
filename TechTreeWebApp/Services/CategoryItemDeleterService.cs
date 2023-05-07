using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class CategoryItemDeleterService : ICategoryItemDeleterService
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemDeleterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteCategoryItem(CategoryItem categoryItem)
        {
            _context.CategoryItem.RemoveRange(categoryItem);
            await _context.SaveChangesAsync();
        }
    }
}
