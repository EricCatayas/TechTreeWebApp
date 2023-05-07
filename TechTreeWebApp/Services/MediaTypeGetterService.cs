using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Data;
using TechTreeWebApp.Entities;
using TechTreeWebApp.ServiceContracts;

namespace TechTreeWebApp.Services
{
    public class MediaTypeGetterService : IMediaTypeGetterService
    {
        private readonly ApplicationDbContext _context;

        public MediaTypeGetterService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public Task<MediaType?> GetMediaTypeById(int? mediaTypeId)
        {
            if (mediaTypeId == null)
                return null;
            return _context.MediaType.FirstOrDefaultAsync(item => item.Id == mediaTypeId);
        }

        public async Task<List<MediaType>?> GetMediaTypes()
        {
            return await _context.MediaType.ToListAsync();
        }
    }
}
