using TechTreeWebApp.Models;

namespace TechTreeWebApp.ServiceContracts
{
    public interface ICategoryItemDetailsGetterService
    {
        Task<IEnumerable<CategoryItemDetailsModel>> GetCategoryItemDetailsForUser(string userId);
        Task<IEnumerable<CategoryItemDetailsModel>> GetAllCategoryItemDetails();
    }
}
