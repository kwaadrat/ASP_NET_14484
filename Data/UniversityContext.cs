using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UniversityContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<InstructorEntity> Instructors { get; set; }
        public DbSet<EnrollmentEntity> Enrollments { get; set; }
        public DbSet<ExamEntity> Exams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var db = System.IO.Path.Join(path, "university.db");
            options.UseSqlite($"Data Source={db}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InstructorEntity>().HasData(
                new InstructorEntity() { Id = 1, Name = "Konrad Ogłaza", AcademicTitle = "mgr inż." }
            );
            modelBuilder.Entity<CourseEntity>().HasData(
                new CourseEntity() { Id = 1, Name = "ASP.NET", Credits = 10, InstructorId = 1 }
            );


            string ADMIN_ID = Guid.NewGuid().ToString();
            string ROLE_ID = Guid.NewGuid().ToString();

            // dodanie roli administratora
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            // utworzenie administratora jako użytkownika
            var admin = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "adminuser@wsei.edu.pl",
                EmailConfirmed = true,
                UserName = "adminuser@wsei.edu.pl",
                NormalizedUserName = "ADMINUSER@WSEI.EDU.PL",
                NormalizedEmail = "ADMINUSER@WSEI.EDU.PL"
            };

            // haszowanie hasła, najlepiej wykonać to poza programem i zapisać gotowy
            // PasswordHash
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = ph.HashPassword(admin, "S3cretPassword");

            // zapisanie użytkownika
            modelBuilder.Entity<IdentityUser>().HasData(admin);

            // przypisanie roli administratora użytkownikowi
            modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
