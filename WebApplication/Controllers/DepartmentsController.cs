using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Data.Repositories;
using WebApplication.Models;
using WebApplication.Services.Interfaces;

namespace WebApplication.Controllers
{
    [Route("Departments")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsService _departmentsService;
        private readonly IFacultiesRepository _facultiesRepository;

        public DepartmentsController(IDepartmentsService departmentsService, IFacultiesRepository facultiesRepository)
        {
            _departmentsService = departmentsService;
            _facultiesRepository = facultiesRepository;
        }

        // GET: Departments
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var result = await _departmentsService.GetManyDepartments();
            return View(result);
        }

        // GET: Departments
        [HttpGet]
        public async Task<IActionResult> ShowSearchForm(string Search)
        {
            return View("Index", await _departmentsService.SearchAsync(Search));
        }

        // GET: Departments/Search
        public IActionResult DepartmentsSearch()
        {
            return View();
        }

        // GET: Departments/Details/5
        [HttpGet("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _departmentsService.GetDepartmentByIdAsync(id.Value);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // GET: Departments/Create
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Faculty = new SelectList(await _facultiesRepository.GetMany().ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Financing,Name,FacultyId")] Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentsService.CreateDepartmentAsync(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _departmentsService.GetDepartmentByIdAsync(id.Value);
            if (departments == null)
            {
                return NotFound();
            }
            return View(departments);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Financing,Name,FacultyId")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentsService.UpdateDepartmentAsync(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Departments = await _departmentsService.GetDepartmentByIdAsync(id.Value);
            if (Departments == null)
            {
                return NotFound();
            }

            return View(Departments);
        }

        // POST: Departments/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departmentsService.DeleteDepartmentAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentsExists(int id)
        {
            var result = _departmentsService.GetDepartmentByIdAsync(id);
            return result != null;
        }
    }
}
