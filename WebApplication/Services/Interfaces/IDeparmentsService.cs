using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Models.Dto;

namespace WebApplication.Services.Interfaces
{
    public interface IDepartmentsService
    {
        Task<IList<Department>> GetManyDepartments();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> CreateDepartmentAsync(Department entity);
        Task<Department> UpdateDepartmentAsync(Department entity);
        Task<Department> DeleteDepartmentAsync(int id);
        Task<ICollection<Department>> SearchAsync(string search);
        Task<ICollection<OverviewDto>> GetOverviewAsync();
    }
}