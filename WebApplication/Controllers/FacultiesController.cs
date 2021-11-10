using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApplication.Data.Repositories;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FacultiesController : Controller
    {
        private readonly IFacultiesRepository _repo;

        public FacultiesController(IFacultiesRepository repo)
        {
            _repo = repo;
        }


        // GET: Faculties
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index", await _repo.GetMany().ToListAsync());
        }

        // GET: Faculties/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculties = await _repo.GetAsync(m => m.Id == id);
            if (faculties == null)
            {
                return NotFound();
            }

            return View(faculties);
        }

        // GET: Faculties/Create
        [HttpGet("Create")]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Faculty faculties)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddAsync(faculties);
                return RedirectToAction(nameof(Index));
            }
            return View(faculties);
        }

        // GET: Faculties/Edit/5
        [HttpGet("Edit/{id}")]
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _repo.GetAsync(f => f.Id == id);
            await _repo.UpdateAsync(entity);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Faculty faculties)
        {
            if (id != faculties.Id)
            {
                return NotFound($"Id is not equal with {nameof(Faculty)}.Id");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateAsync(faculties);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FacultiesExistsAsync(faculties.Id))
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
            return View(faculties);
        }

        // GET: Faculties/Delete/5
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculties = await _repo.GetAsync(f => f.Id == id);
            if (faculties == null)
            {
                return NotFound();
            }

            return View(faculties);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculties = await _repo.GetAsync(f => f.Id == id);
            if (faculties == null) return NotFound($"Record with id: {id} is not found");
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private Task<bool> FacultiesExistsAsync(int id)
        {
            return _repo.AnyAsync(e => e.Id == id);
        }
    }
}
