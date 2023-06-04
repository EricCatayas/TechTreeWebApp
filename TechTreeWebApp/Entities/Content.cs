using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTreeWebApp.Entities
{
    public class Content
    {
        public int Id { get; set; }
        [Required][StringLength(150, MinimumLength = 2)] 
        public string? Title { get; set; } 
        [Display(Name = "Html Link")]
        public string? HTMLContent { get; set; } 
        [Display(Name = "Video Link")]
        public string? VideoLink { get; set; }
        [ForeignKey("CategoryItemId")]
        public virtual CategoryItem? CategoryItem { get; set; } //Establishing one-to-one relationship
        public int? CategoryItemId { get; set;}
        //N: This property cannot be 
        //named CategoryItemId because this would
        //interfere with future migrations
        //It has been named like this
        //so as not to conflict with EF Core naming conventions
        [NotMapped]
        public int CategoryId { get; set;}
    }
}
