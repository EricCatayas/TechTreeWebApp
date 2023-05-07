using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class CategoriesDeleterService : ICategoriesDeleterService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesDeleterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteCategory(Category category)
        {
            _context.RemoveRange(category);
            await _context.SaveChangesAsync();
        }
    }
}
