using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CourseManagement.DataAccess.Data
{
    public class CourseManagementDb : IdentityDbContext<AppUser>
    {
        public CourseManagementDb(DbContextOptions<CourseManagementDb> options)
            : base(options) { }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Enrollment> enrollments { get; set; }
        public DbSet<CourseLearningOutcome> CourseLearningOutcomes { get; set; }
        public DbSet<LessonProgress> LessonProgresses { get; set; }
        public DbSet<CourseProgress> CourseProgresses { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Lesson)
                .WithMany(l => l.Comments)
                .HasForeignKey(c => c.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.UserId, e.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Blogs)
                .UsingEntity(j => j.ToTable("BlogCategory"));

            modelBuilder.Entity<LessonProgress>()
                .HasKey(e => new { e.UserId, e.LessonId });

            modelBuilder.Entity<LessonProgress>()
                .HasOne(e => e.User)
                .WithMany(u => u.LessonProgresses)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<LessonProgress>()
                .HasOne(e => e.Lesson)
                .WithMany(c => c.LessonProgresses)
                .HasForeignKey(e => e.LessonId);

            modelBuilder.Entity<CourseLearningOutcome>()
                .HasOne(clo => clo.Course)
                .WithMany(c => c.LearningOutcomes)
                .HasForeignKey(clo => clo.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseProgress>()
            .HasOne(cp => cp.User)
            .WithMany(u => u.CourseProgresses)
            .HasForeignKey(cp => cp.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseProgress>()
                .HasOne(cp => cp.Course)
                .WithMany()
                .HasForeignKey(cp => cp.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseProgress>()
                .HasOne(cp => cp.LastViewedLesson)
                .WithMany()
                .HasForeignKey(cp => cp.LastViewedLessonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseProgress>()
                .HasIndex(cp => new { cp.UserId, cp.CourseId })
                .IsUnique();

            modelBuilder.Entity<Note>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Lesson)
                .WithMany()
                .HasForeignKey(n => n.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            DataSeed.InsertData(modelBuilder);
        }

    }
}