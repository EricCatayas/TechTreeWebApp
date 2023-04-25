namespace TechTreeWebApp.Models
{
    /// <summary>
    /// Store data for category along w/ the categoryItems
    /// E.g. Advance C# Category + the CategoryItems 
    /// </summary>
    public class GroupedCategoryItemsByCategoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public IGrouping<int, CategoryItemDetailsModel>? Items { get; set; }
    }
}
