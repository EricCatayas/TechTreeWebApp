using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class ContentGetterService : IContentGetterService
    {
        private readonly ApplicationDbContext _context;
        public ContentGetterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Content?> GetContentByCategoryItemID(int categoryItemId)
        {
            Content? content = await(from _content in _context.Content
                                    where _content.CategoryItem.Id == categoryItemId //Mistake: _content.CatItemId when the specified member is unmapped
                                    select new Content
                                    {
                                        Title = _content.Title,
                                        VideoLink = _content.VideoLink,
                                        HTMLContent = _content.HTMLContent,
                                    }).FirstOrDefaultAsync();
            return content;
        }

        public Task<Content?> GetContentByID(int contentId)
        {
            return _context.Content.FirstOrDefaultAsync(temp => temp.Id== contentId);
        }
    }
}
