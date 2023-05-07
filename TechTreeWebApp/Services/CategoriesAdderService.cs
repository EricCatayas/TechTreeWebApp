using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class CategoriesAdderService : ICategoriesAdderService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesAdderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
        }
    }
}
