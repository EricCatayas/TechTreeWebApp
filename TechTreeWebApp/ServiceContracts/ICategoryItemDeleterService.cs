using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface ICategoryItemDeleterService
    {
        Task DeleteCategoryItem(CategoryItem categoryItem);
    }
}
