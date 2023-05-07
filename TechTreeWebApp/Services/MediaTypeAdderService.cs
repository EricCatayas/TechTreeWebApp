using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class MediaTypeAdderService : IMediaTypeAdderService
    {
        private readonly ApplicationDbContext _context;

        public MediaTypeAdderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddMediaType(MediaType mediaType)
        {
            _context.MediaType.Add(mediaType);
            await _context.SaveChangesAsync();
        }
    }
}
