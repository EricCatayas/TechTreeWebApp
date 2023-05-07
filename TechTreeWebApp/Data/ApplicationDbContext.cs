using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
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
        public string? FirstName { get; set; }
        [StringLength(150)]
        public string? LastName { get; set; }
        [StringLength(150)]
        public string? Address1 { get; set; }

        [StringLength(150)] 
        public string? Address2 { get; set; }
        [StringLength(30)]
        public string? PostCode { get; set; }
        [ForeignKey("UserId")]
        public ICollection<UserCategory>? UserCategories { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryItem> CategoryItem { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<UserCategory> UserCategory { get; set; }
        public DbSet<Content> Content { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CategoryItem>()
                .HasOne(ci => ci.Category)
                .WithMany(ca => ca.CategoryItems)
                .HasForeignKey(ci => ci.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Content>()
                .HasOne(co => co.CategoryItem)
                .WithOne(ca => ca.Content)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserCategory>()
                .HasOne<Category>()
                .WithMany(ca => ca.UserCategories)
                .HasForeignKey(uc => uc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}