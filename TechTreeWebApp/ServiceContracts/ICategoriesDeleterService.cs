using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface ICategoriesDeleterService
    {
        Task DeleteCategory(Category category);
    }
}
