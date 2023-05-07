using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class CategoryItemAdderService : ICategoryItemAdderService
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemAdderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCategoryItem(CategoryItem categoryItem)
        {
            _context.CategoryItem.Add(categoryItem);
            await _context.SaveChangesAsync();
        }
    }
}
