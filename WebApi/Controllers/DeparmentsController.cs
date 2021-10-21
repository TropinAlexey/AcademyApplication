using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Data.Repositories;
using WebApplication.Models;

namespace WebApi.Controllers
{
    [Route("Departments")]
    [ApiController]
    public class DeparmentsController : ControllerBase
    {
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly ILogger<DeparmentsController> _logger;

        public DeparmentsController(ILogger<DeparmentsController> logger, IDepartmentsRepository departmentsRepository)
        {
            _logger = logger;
            _departmentsRepository = departmentsRepository;
        }

        [HttpGet]
        public IQueryable<Department> GetMany()
        {
            _logger.LogWarning("Test message");
            var result = _departmentsRepository.GetMany();
            return result;
        }
    }
}
