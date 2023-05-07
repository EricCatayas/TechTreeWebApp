using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface IMediaTypeGetterService
    {
        Task<MediaType?> GetMediaTypeById(int? mediaTypeId);
        Task<List<MediaType>?> GetMediaTypes();
    }
}
