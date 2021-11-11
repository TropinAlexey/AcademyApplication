using WebApplication.Models;

namespace WebApplication.Data.Repositories
{
    public class TeachersRepository : GenericRepository<Teacher>, ITeachersRepository
    {
        private readonly ApplicationDbContext _context;

        public TeachersRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}