using WebApplication.Models;

namespace WebApplication.Data.Repositories
{
    public class SubjectsRepository : BaseRepository<Subject>, ISubjectsRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}