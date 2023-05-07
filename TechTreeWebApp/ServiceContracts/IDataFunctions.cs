using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface IDataFunctions
    {
        Task UpdataUserCategoryEntityAsync(List<UserCategory> userCategoriesToAdd, List<UserCategory> userCategoriesToDelete);
    }
}
