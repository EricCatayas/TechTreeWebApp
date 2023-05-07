using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface IContentGetterService
    {
        Task<Content?> GetContentByCategoryItemID(int categoryItemId);
        Task<Content?> GetContentByID(int contentId);
    }
}
