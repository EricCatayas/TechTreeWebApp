using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTreeWebApp.Entities
{
    public class CategoryItem
    {
        private DateTime _releasedDate = DateTime.MinValue;
        public int Id { get; set; }
        [Required][StringLength(150, MinimumLength = 2)]
        public string? Title { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Id")]
        public virtual Category? Category { get; set; }
        [ForeignKey("CategoryItemId")]
        public virtual Content? Content { get; set; }
        [NotMapped] // forcing entity framework to gnore MediaType's props
        public virtual ICollection<SelectListItem>? MediaTypes { get; set; }
        [Required(ErrorMessage = "Please select an item from the '{0}' drop down menu")]
        [Display(Name = "Media Type")]
        public int MediaTypeId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        [Display(Name = "Released Date")]
        public DateTime DateItemReleased 
        {
            get 
            {
                return (_releasedDate == DateTime.MinValue) ? DateTime.Now : _releasedDate;
            }
            set
            {
                _releasedDate = value;
            }
        }
        public int ContentId { get; set; } // Do not remove
    }
}
