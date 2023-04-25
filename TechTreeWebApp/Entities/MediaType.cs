using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechTreeWebApp.Interfaces;

namespace TechTreeWebApp.Entities
{
    public class MediaType : IPrimaryProperties
    {
        public int Id { get; set; }
        [Required][StringLength(150, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;
        [Display(Name = "Thumbnail Image Path")]
        public string ThumbnailImagePath { get; set; } = string.Empty;
        [ForeignKey("MediaTypeId")]
        public virtual ICollection<CategoryItem>? CategoryItems { get; set; }
    }
}
