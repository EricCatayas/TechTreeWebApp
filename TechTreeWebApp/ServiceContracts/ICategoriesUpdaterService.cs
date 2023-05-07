using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface ICategoriesUpdaterService
    {
        Task UpdateCategory(Category category);
    }
}
