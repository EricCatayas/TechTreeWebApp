using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTreeWebApp.Entities
{
    public class Content
    {
        public int Id { get; set; }
        [Required][StringLength(150, MinimumLength = 2)] 
        public string Title { get; set; } = string.Empty;
        [Display(Name = "Html Link")]
        public string HTMLContent { get; set; } = string.Empty;
        [Display(Name = "Video Link")]
        public string VideoLink { get; set; } = string.Empty;
        public CategoryItem? CategoryItem { get; set; } //Establishing one-to-one relationship
        [NotMapped]// Not mapped to the fields of the same name -- Removed [NotMapped] because I cannot retrievee content.CategoryItem.Id in ContentController.Index() -- Lol! Logic Error hehe somehow
        public int CatItemId { get; set;}
        //N: This property cannot be 
        //named CategoryItemId because this would
        //interfere with future migrations
        //It has been named like this
        //so as not to conflict with EF Core naming conventions
        [NotMapped]
        public int CategoryId { get; set;}
    }
}
