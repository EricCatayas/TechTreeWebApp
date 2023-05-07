using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface IContentAdderService
    {
        Task AddContent(Content content);
    }
}
