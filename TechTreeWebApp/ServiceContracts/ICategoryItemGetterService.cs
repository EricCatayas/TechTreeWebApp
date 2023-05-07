using System.Linq.Expressions;
using TechTreeWebApp.Entities;

namespace TechTreeWebApp.ServiceContracts
{
    public interface ICategoryItemGetterService
    {
        Task<CategoryItem?> GetCategoryItemById(int? catItemId);
        Task<List<CategoryItem>?> GetCategoryItemByCategoryId(int? categoryId);
        Task<List<CategoryItem>?> GetAllCategoryItems();
    }
}
