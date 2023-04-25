using TechTreeWebApp.Entities;

namespace TechTreeWebApp.Data
{
    public interface IDataFunctions
    {
        Task UpdataUserCategoryEntityAsync(List<UserCategory> userCategoriesToAdd, List<UserCategory> userCategoriesToDelete);
    }
}
