using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface ICategoriesAdderService
    {
        Task AddCategory(Category category);
    }
}
