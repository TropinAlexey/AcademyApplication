using WebApplication.Models;

namespace WebApplication.Data.Repositories
{
    public class GroupLectureRepository : GenericRepository<GroupLecture>, IGroupLectureRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupLectureRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}