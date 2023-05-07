using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface IMediaTypeAdderService
    {
        Task AddMediaType(MediaType mediaType);
    }
}
