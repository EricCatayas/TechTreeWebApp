using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface ICategoriesToUserGetterServices
    {
        List<UserCategory> GetCategoriesToAddForUser(string[] categoryIds, string userId);
        Task<List<Category>> GetCategoriesThatHaveContent();
        Task<List<Category>> GetCategoriesCurrentlySavedForUser(string userId);
        List<UserCategory> GetCategoriesToDeleteForUser(string userId);
    }
}
