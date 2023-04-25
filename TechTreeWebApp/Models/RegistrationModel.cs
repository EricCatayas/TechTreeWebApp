using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace TechTreeWebApp.Models
{
    public class RegistrationModel
    {
        [Required]
        [DisplayName("Username")]
        [StringLength(120, MinimumLength = 4)]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Email Address")]
        [StringLength(120, MinimumLength = 4)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [StringLength(120, MinimumLength = 5)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Invalid. Reconfirm password ")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        [DisplayName("First Name")]
        [StringLength(100, MinimumLength =2)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [DisplayName("Last Name")]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Address1 { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Address2 { get; set; } = string.Empty;
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [DisplayName("Post Code")]
        public string PostCode { get; set; } = string.Empty;
        [Required]
        [DisplayName("Phone No.")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public bool AcceptTermsAndAgreement { get; set; } = false;
        public string RegistrationInValid { get; set; } = string.Empty; //bool flag for jQuery

        public int CategoryId {  get; set; } 
    }
}
