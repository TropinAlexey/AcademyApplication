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

        public DbSet<Departaments> Departaments { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
    }
}