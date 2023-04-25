namespace TechTreeWebApp.Areas.Admin.Models
{
    public class UserModel
    {
        public string Id { get; set; } //Why string? N: class UserCategory containing string UserId and CategoryItemId
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
