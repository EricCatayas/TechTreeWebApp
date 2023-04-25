namespace TechTreeWebApp.Areas.Admin.Models
{
    public class UsersCategoryListModel
    {
        public int CategoryId { get; set; }
        public ICollection<UserModel>? UsersCollection { get; set; }
        public ICollection<UserModel>? UsersSelected { get; set; }
    }
}
