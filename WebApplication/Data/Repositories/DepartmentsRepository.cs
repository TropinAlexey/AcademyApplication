using WebApplication.Models;

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