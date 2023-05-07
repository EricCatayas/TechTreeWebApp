using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface IMediaTypeDeleterService
    {
        Task DeleteMediaType(MediaType mediaType);
    }
}
