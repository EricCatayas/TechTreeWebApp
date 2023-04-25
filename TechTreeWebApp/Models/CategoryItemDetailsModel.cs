namespace TechTreeWebApp.Models
{
    // User-defined type for representing details of category/item

    public class CategoryItemDetailsModel
    {
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; } = string.Empty;
        public int CategoryItemId { get; set; }

        public string CategoryItemTitle { get; set; } = string.Empty;
        public string CategoryItemDescription { get; set; } = string.Empty; 
        public string MediaImagePath { get; set; } = string.Empty;

    }
}
