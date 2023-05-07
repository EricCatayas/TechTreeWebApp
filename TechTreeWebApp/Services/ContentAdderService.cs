using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class ContentAdderService : IContentAdderService
    {
        private readonly ApplicationDbContext _context;

        public ContentAdderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddContent(Content content)
        {
            _context.Content.Add(content);
            await _context.SaveChangesAsync();
        }
    }
}
