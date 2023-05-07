using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface ICategoryItemUpdaterService
    {
        Task UpdateCategoryItem(CategoryItem categoryItem);
    }
}
