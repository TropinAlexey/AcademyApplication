using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data.Repositories;
using WebApplication.Models;
using WebApplication.Models.Dto;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly IFacultiesRepository _facultiesRepository;
        private string _rate;

        public DepartmentsService(IDepartmentsRepository departmentsRepository,
            IFacultiesRepository facultiesRepository,
            ICurrencyExchangeService currencyExchangeService)
        {
            _departmentsRepository = departmentsRepository;
            _facultiesRepository = facultiesRepository;
            _rate = currencyExchangeService.GetCurrencyExchangeRateAsync("USD", "EUR", 1).GetAwaiter().GetResult();
        }

        public async Task<IList<Department>> GetManyDepartments()
        {
            var result = await _departmentsRepository.GetMany().ToListAsync();

            if (decimal.TryParse(_rate, NumberStyles.Any, CultureInfo.InvariantCulture, out var rateDecimal))
            {
                foreach (var department in result)
                {
                    department.Financing *= rateDecimal;
                }
            }
            return result;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var result = await _departmentsRepository.GetNotTrackedAsync(d => d.Id == id);
            if (result != null)
            {
                result.Faculty = await _facultiesRepository.GetNotTrackedAsync(f => f.Id == result.FacultyId);
            }
            return result;
        }

        public async Task<Department> CreateDepartmentAsync(Department entity)
        {
            entity.Id = 0;
            var result = await _departmentsRepository.AddAsync(entity);
            return result;
        }

        public async Task<Department> UpdateDepartmentAsync(Department entity)
        {
            var result = await _departmentsRepository.UpdateAsync(entity);
            return result;
        }

        public async Task<Department> DeleteDepartmentAsync(int id)
        {
            var result = await _departmentsRepository.DeleteAsync(id);
            return result;
        }

        public async Task<ICollection<Department>> SearchAsync(string search)
        {
            var result = await _departmentsRepository.GetMany()
                .Where(d => d.Name.Contains(search) || d.Financing == Convert.ToDecimal(search) || d.Id.ToString() == search).ToListAsync();
            return result;
        }

        public async Task<ICollection<OverviewDto>> GetOverviewAsync()
        {
            var result = await _facultiesRepository.GetOverview().OrderBy(f => f.FacultyName).ToArrayAsync();
            if (decimal.TryParse(_rate, NumberStyles.Any, CultureInfo.InvariantCulture, out var rateDecimal))
            {
                foreach (var department in result)
                {
                    department.Financing *= rateDecimal;
                }
            }
            return result;
        }
    }
}