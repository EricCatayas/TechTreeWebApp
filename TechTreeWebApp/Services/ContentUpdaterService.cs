using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class ContentUpdaterService : IContentUpdaterService
    {
        private readonly ApplicationDbContext _context;

        public ContentUpdaterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task UpdateContent(Content content)
        {
            _context.Update(content);
            await _context.SaveChangesAsync();
        }
    }
}
