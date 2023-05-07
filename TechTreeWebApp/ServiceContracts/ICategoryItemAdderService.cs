using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface ICategoryItemAdderService
    {
        Task AddCategoryItem(CategoryItem categoryItem);
    }
}
