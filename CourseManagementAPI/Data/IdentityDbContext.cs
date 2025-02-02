using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CourseManagementAPI.Entity;

namespace CourseManagementAPI.Data
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
            
        }

        public DbSet<Blog>  Blogs { get; set; }
    }
}
