using WebApplication.Models;

namespace WebApplication.Data.Repositories
{
    public class GroupsRepository : GenericRepository<Group>, IGroupsRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}