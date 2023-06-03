using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace TechTreeWebApp.Data.Migrations
{
    public partial class forConteeeennt : Migration
    {
        const string ADMIN_USER_GUID = "8042cfad-8e75-4564-8e1d-54ec640adafb";
        const string ADMIN_ROLE_GUID = "d6422b1d-6e1c-4191-8f87-aae20ee7e142";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Inserting to AspNetUsers db in SQL
            // Add useraccount to system, then admin role to system , useraccount to the admin role
            var hasher = new PasswordHasher<ApplicationUser>(); //Used for verifying integrity of the password
            var passwordHash = hasher.HashPassword(null, "Admin1!"); //TODO Hide Password

            StringBuilder stringBuilder = new StringBuilder(); //Building the sql statement

            stringBuilder.AppendLine("INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName,Email,NormalizedEmail,PasswordHash,SecurityStamp,EmailConfirmed, PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount)");
            stringBuilder.AppendLine("VALUES(");
            stringBuilder.AppendLine($"'{ADMIN_USER_GUID}'");
            stringBuilder.AppendLine(",'catayasericjay@gmail.com'");
            stringBuilder.AppendLine(",'CATAYASERICJAY@GMAIL.COM'"); //are used to validate the case insensitive uniqueness of the UserName and Email fields
            stringBuilder.AppendLine(",'catayasericjay@gmail.com'");
            stringBuilder.AppendLine(",'CATAYASERICJAY@GMAIL.COM'");
            stringBuilder.AppendLine($", '{passwordHash}'"); 
            stringBuilder.AppendLine(", ''");//Security Stamp
            stringBuilder.AppendLine(", 0"); //Email Confirmed : True/False
            stringBuilder.AppendLine(", 0"); //PhoneNumber Confirmed    
            stringBuilder.AppendLine(", 0"); //Two Factor
            stringBuilder.AppendLine(", 0"); //LockOut Enabled
            stringBuilder.AppendLine(", 0");
           /* stringBuilder.AppendLine(",'Admin'"); //First Name
            stringBuilder.AppendLine(",'Catayas'"); //Last Name
            stringBuilder.AppendLine(",'0053'"); //Address1
            stringBuilder.AppendLine(",'Mahayahay Street, Gairan, Bogo City'"); //Address2
            stringBuilder.AppendLine(",'6010'");*/ // Postal Code
            stringBuilder.AppendLine(")"); 

            migrationBuilder.Sql(stringBuilder.ToString());

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}','Admin','ADMIN')");
            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}','{ADMIN_ROLE_GUID}')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //Removing rows from the Up(); Will only be called if you update-database a previous Migration
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{ADMIN_USER_GUID}' AND RoleId = '{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{ADMIN_USER_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{ADMIN_ROLE_GUID}'");
        }
    }
}
