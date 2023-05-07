using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface ICategoriesGetterService
    {
        Task<Category?> GetCategoryById(int? Id);
        Task<List<Category>?> GetAllCategories();
    }
}
