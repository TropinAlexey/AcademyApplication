using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication.Models.Departaments> Departaments { get; set; }
        public DbSet<WebApplication.Models.Faculties> Faculties { get; set; }
        public DbSet<WebApplication.Models.Groups> Groups { get; set; }
    }
}