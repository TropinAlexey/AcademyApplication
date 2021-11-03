using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data.Repositories;
using WebApplication.Models;
using WebApplication.Models.Dto;
using WebApplication.Models.FilterModels;
using WebApplication.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("Departments")]
    [ApiController]
    public class DeparmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;
        private readonly IFacultiesRepository _facultiesRepository;

        public DeparmentsController(ILogger<DeparmentsController> logger, IDepartmentsService departmentsService, IFacultiesRepository facultiesRepository)
        {
            _departmentsService = departmentsService;
            _facultiesRepository = facultiesRepository;
        }

        [HttpGet("many")]
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

        [HttpGet("Overview")]
        public IActionResult GetManyFaculties([FromQuery] OverviewFilterModel filterModel)
        {
            var result = _facultiesRepository.GetOverview();
            if (filterModel.Financing != null) result = result.Where(r => r.Financing == filterModel.Financing);
            if (filterModel.TeacherSurname != null) result = result.Where(r => r.TeacherSurname.Contains(filterModel.TeacherSurname));
            if (filterModel.SubjectName != null) result = result.Where(r => r.SubjectName == filterModel.SubjectName);
            if (filterModel.EmploymentDate != null) result = result.Where(r => r.EmploymentDate == filterModel.EmploymentDate);
            return Ok(result.Skip(filterModel.Skip).Take(filterModel.Take));
        }
    }
}