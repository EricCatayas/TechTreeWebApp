using System.ComponentModel.DataAnnotations.Schema;

namespace TechTreeWebApp.Entities
{
    /// <summary>
    /// The relationship between Category and Users is established by this database
    /// </summary>
    public class UserCategory
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int CategoryId { get; set; }        
    }
}
