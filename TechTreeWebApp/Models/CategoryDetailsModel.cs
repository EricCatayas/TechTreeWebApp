using TechTreeWebApp.Entities;

namespace TechTreeWebApp.Models
{
    /// <summary>
    ///     Storing a collection of grouped categoryItems by Category Models
    /// </summary>
    public class CategoryDetailsModel
    {
        public IEnumerable<GroupedCategoryItemsByCategoryModel>? GroupedCategoryItemsByCategoryModels { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
    }
}
