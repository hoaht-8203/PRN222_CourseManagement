using CourseManagement.Model.Constant;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.DataAccess.Data
{
    public class DataSeed
    {
        public static void InsertData(ModelBuilder modelBuilder)
        {
          
            var adminRoleId = Guid.NewGuid().ToString();
            var customerRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = Role.Role_User_Admin, ConcurrencyStamp = "1", NormalizedName = Role.Role_User_Admin.ToUpper() },
                new IdentityRole { Id = customerRoleId, Name = Role.Role_User_Customer, ConcurrencyStamp = "2", NormalizedName = Role.Role_User_Customer.ToUpper() }
            );


            var adminUserId = Guid.NewGuid().ToString();
            var customerUserId = Guid.NewGuid().ToString();

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = adminUserId,
                    FullName = "Admin",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Admin123."), 
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                },
                new AppUser
                {
                    Id = customerUserId,
                    FullName = "User",
                    UserName = "user@user.com",
                    NormalizedUserName = "USER@USER.COM",
                    Email = "user@user.com",
                    NormalizedEmail = "USER@USER.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "User123."), 
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                }
            );

            // Assign roles to users
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = customerUserId,
                    RoleId = customerRoleId
                }
            );
        }
    }
}
