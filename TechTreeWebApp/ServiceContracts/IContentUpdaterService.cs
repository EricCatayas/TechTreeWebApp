using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface IContentUpdaterService
    {
        Task UpdateContent(Content content);
    }
}
