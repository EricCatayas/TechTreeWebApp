using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechTreeWebApp.Entities;

namespace TechTreeWebApp.Data
{
    /// <summary>
    /// IdentityDbContext facilitates code-first integration of asp.net identity to our db
    ///     via Code-First Migration
    /// We extend IdentityUser by including the fields below
    /// </summary>    
    public class ApplicationUser : IdentityUser
    {
        [StringLength(150)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(150)]
        public string LastName { get; set; } = string.Empty;
        [StringLength(150)]
        public string Address1 { get; set; } = string.Empty;

        [StringLength(150)] 
        public string Address2 { get; set; } = string.Empty;
        [StringLength(30)]
        public string PostCode { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public ICollection<UserCategory>? UserCategories { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // Framework must know the new Models to generate the corresponding db
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryItem> CategoryItem { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<UserCategory> UserCategory { get; set; }
        public DbSet<Content> Content { get; set; }
    }
}