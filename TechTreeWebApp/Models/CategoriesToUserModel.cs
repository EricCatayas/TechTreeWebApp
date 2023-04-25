using TechTreeWebApp.Entities;

namespace TechTreeWebApp.Models
{
    public class CategoriesToUserModel
    {
        public string? UserId { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Category>? SelectedCategories { get; set; } //represented as Checkboxes the are checked 
    }
}
