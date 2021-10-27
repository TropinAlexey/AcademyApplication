using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("Departments")]
    [ApiController]
    public class DeparmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;

        public DeparmentsController(ILogger<DeparmentsController> logger, IDepartmentsService departmentsService)
        {
            _departmentsService = departmentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMany()
        {
            var result = await _departmentsService.GetManyDepartments();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _departmentsService.GetDepartmentByIdAsync(id);
            if (result == null) return NotFound($"Record with id: {id} is not found");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department entity)
        {
            try
            {
                var result = await _departmentsService.CreateDepartmentAsync(entity);
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
                var result = await _departmentsService.UpdateDepartmentAsync(entity);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = _departmentsService.DeleteDepartmentAsync(id);
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