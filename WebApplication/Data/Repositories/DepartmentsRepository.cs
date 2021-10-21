using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication.Models;
using WebApplication.Models.Dto;

namespace WebApplication.Data.Repositories
{
    public class DepartmentsRepository : BaseRepository<Department>, IDepartmentsRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}