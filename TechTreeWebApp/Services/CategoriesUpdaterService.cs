using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class CategoriesUpdaterService : ICategoriesUpdaterService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesUpdaterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task UpdateCategory(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
