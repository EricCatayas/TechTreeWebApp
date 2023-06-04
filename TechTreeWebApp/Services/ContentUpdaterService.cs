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
            Content? content_ToUpdate = _context.Content.FirstOrDefault(temp=> temp.Id == content.Id);
            if (content_ToUpdate != null) 
            {
                content_ToUpdate.HTMLContent = content.HTMLContent;
                content_ToUpdate.Title = content.Title;
                content_ToUpdate.VideoLink = content.VideoLink;
                _context.Update(content_ToUpdate);
                await _context.SaveChangesAsync();
            }
        }
    }
}
