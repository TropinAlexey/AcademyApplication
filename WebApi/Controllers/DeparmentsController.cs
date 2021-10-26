using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data.Repositories;
using WebApplication.Models;

namespace WebApi.Controllers
{
    [Route("Departments")]
    [ApiController]
    public class DeparmentsController : ControllerBase
    {
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly IFacultiesRepository _facultiesRepository;

        public DeparmentsController(ILogger<DeparmentsController> logger, IDepartmentsRepository departmentsRepository, IFacultiesRepository facultiesRepository)
        {
            _departmentsRepository = departmentsRepository;
            _facultiesRepository = facultiesRepository;
        }

        [HttpGet]
        public IQueryable<Department> GetMany()
        {
            var result = _departmentsRepository.GetMany();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _departmentsRepository.GetNotTrackedAsync(d => d.Id == id);
            if (result != null)
            {
                result.Faculty = await _facultiesRepository.GetNotTrackedAsync(f => f.Id == result.FacultyId);
            }

            if (result == null) return NotFound($"Record with id: {id} is not found");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department entity)
        {
            try
            {
                entity.Id = 0;
                var result = await _departmentsRepository.AddAsync(entity);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Department entity)
        {
            try
            {
                var result = await _departmentsRepository.UpdateAsync(entity);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            try
            {
                var result = await _departmentsRepository.DeleteAsync(id);
                if (result == null) return NotFound($"Department with id: {id} is not found");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}