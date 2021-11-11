using WebApplication.Models;

namespace WebApplication.Data.Repositories
{
    public class LecturesRepository : GenericRepository<Lecture>, ILecturesRepository
    {
        private readonly ApplicationDbContext _context;

        public LecturesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}