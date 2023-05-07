using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class CategoriesGetterService : ICategoriesGetterService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesGetterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>?> GetAllCategories()
        {
            return await _context.Category.ToListAsync();
        }

        public Task<Category?> GetCategoryById(int? Id)
        {
            if (Id == null)
                return null;
            return _context.Category.FirstOrDefaultAsync(item => item.Id == Id);
        }
    }
}
