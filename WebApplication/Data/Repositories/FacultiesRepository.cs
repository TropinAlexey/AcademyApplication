using WebApplication.Models;

namespace WebApplication.Data.Repositories
{
    public class FacultiesRepository : BaseRepository<Faculty>, IFacultiesRepository
    {
        private readonly ApplicationDbContext _context;

        public FacultiesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}