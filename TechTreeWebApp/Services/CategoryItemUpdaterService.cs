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
            _context.Update(categoryItem);
            await _context.SaveChangesAsync();
        }
    }
}
