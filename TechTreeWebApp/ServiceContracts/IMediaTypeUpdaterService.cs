using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface IMediaTypeUpdaterService
    {
        Task UpdateMediaType(MediaType mediaType);
    }
}
