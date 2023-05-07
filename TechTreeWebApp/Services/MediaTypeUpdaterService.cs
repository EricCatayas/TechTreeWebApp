using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class MediaTypeUpdaterService : IMediaTypeUpdaterService
    {
        private readonly ApplicationDbContext _context;

        public MediaTypeUpdaterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task UpdateMediaType(MediaType mediaType)
        {
            _context.Update(mediaType);
            await _context.SaveChangesAsync();
        }
    }
}
