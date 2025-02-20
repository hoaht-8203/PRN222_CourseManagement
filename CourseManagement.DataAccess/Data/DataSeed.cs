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
          
            var adminRoleId = "11ea7f26-3a0b-4ee8-88ec-0bee65ad975d";
            var customerRoleId = "394e5930-426c-440d-a9f1-fc3a6c59ccf6";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = Role.Role_User_Admin, ConcurrencyStamp = "1", NormalizedName = Role.Role_User_Admin.ToUpper() },
                new IdentityRole { Id = customerRoleId, Name = Role.Role_User_Customer, ConcurrencyStamp = "2", NormalizedName = Role.Role_User_Customer.ToUpper() }
            );


            var adminUserId = "679d2483-e7ed-4db9-8a2c-fa8fe84e06e2";
            var customerUserId = "e404e498-c930-4a45-9b22-c35d2f333d37";

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
