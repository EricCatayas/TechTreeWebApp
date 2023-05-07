using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class MediaTypeDeleterService : IMediaTypeDeleterService
    {
        private readonly ApplicationDbContext _context;

        public MediaTypeDeleterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteMediaType(MediaType mediaType)
        {
            _context.MediaType.RemoveRange(mediaType);
            await _context.SaveChangesAsync();
        }
    }
}
