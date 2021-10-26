using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Departments");
                entity.HasIndex(p => p.Name);
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculties");
                entity.HasIndex(p => p.Name);
                entity.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Groups");
                entity.HasIndex(p => p.Name);
                entity.HasIndex(p => p.Rating);
                entity.HasIndex(p => p.Year);
                entity.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teachers");
                entity.HasIndex(p => p.Name);
                entity.HasIndex(p => p.EmploymentDate);
                entity.HasIndex(p => new {p.Premium, p.Salary});
                entity.Property(p => p.Name).IsRequired().HasMaxLength(250);
                entity.Property(p => p.Surname).IsRequired().HasMaxLength(250);
                entity.Property(p => p.Premium).HasColumnType("money");
                entity.Property(p => p.Salary).HasColumnType("money");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subjects");
                entity.HasIndex(p => p.Name);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(500);
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.ToTable("Lectures");
                entity.HasIndex(p => p.LectureRoom);
                entity.Property(p => p.LectureRoom).HasMaxLength(250);
            });

            modelBuilder.Entity<GroupLecture>(entity =>
            {
                entity.ToTable("GroupsLectures");
                entity.HasKey(p => p.Id);
                entity.HasIndex(p => p.GroupId);
                entity.HasIndex(p => p.LectureId);

                entity.Property(p => p.GroupId);
                entity.Property(p => p.LectureId);
                entity.Property(p => p.DayOfWeek);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<GroupLecture> GroupsLectures { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}