using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechTreeWebApp.Interfaces;

namespace TechTreeWebApp.Entities
{
    public class Category : IPrimaryProperties // @model IEnumerable<Category> defined in UsersToCategory.Index.cshtml && called w/ ConvertToSelectList()
    {
        public int Id { get; set; }
        [Required][StringLength(150, MinimumLength =2)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Thumbnail Image Path")]
        public string ThumbnailImagePath { get; set; } = string.Empty;
        [ForeignKey("CategoryId")] //(Referencial Integrity i.e relationship)
        ICollection<CategoryItem>? CategoryItems { get; set; }
        [ForeignKey("CategoryId")]
        ICollection<UserCategory>? UserCategories { get; set; }
    }
}
