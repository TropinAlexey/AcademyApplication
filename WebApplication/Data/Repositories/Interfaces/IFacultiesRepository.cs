using System.Linq;
using WebApplication.Models;
using WebApplication.Models.Dto;

namespace WebApplication.Data.Repositories
{
    public interface IFacultiesRepository : IBaseRepository<Faculty>
    {
        IQueryable<OverviewDto> GetOverview();
    }
}