using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services.Interfaces;

namespace WebApplication.Controllers
{
    [Route("Lectures")]
    public class LectureController : Controller
    {
        private readonly ILectureService _lectureService;

        public LectureController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        // GET: Lectures
        [Route("/Index")]
        public async Task<IActionResult> Index()
        {
            var result = await _lectureService.GetManyLecturesAsync();
            return View(result);
        }

        // GET: Lectures
        public async Task<IActionResult> ShowSearchForm(string Search)
        {
            return View("Index", await _lectureService.SearchAsync(Search));
        }

        // GET: Lectures/Search
        public IActionResult LecturesSearch()
        {
            return View("ShowSearchForm");
        }

        // GET: Lectures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Lectures = await _lectureService.GetLectureIdAsync(id.Value);
            if (Lectures == null)
            {
                return NotFound();
            }

            return View(Lectures);
        }

        // GET: Lectures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lectures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LectureRoom,SubjectId,TeacherId")] Lecture entity)
        {
            if (ModelState.IsValid)
            {
                await _lectureService.CreateLectureAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        // GET: Lectures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _lectureService.GetLectureIdAsync(id.Value);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // POST: Lectures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Financing,Name,FacultyId")] Lecture entity)
        {
            if (id != entity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _lectureService.UpdateLectureAsync(entity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturesExists(entity.Id))
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
            return View(entity);
        }

        // GET: Lectures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entities = await _lectureService.GetLectureIdAsync(id.Value);
            if (entities == null)
            {
                return NotFound();
            }

            return View(entities);
        }

        // POST: Lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _lectureService.DeleteLectureAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LecturesExists(int id)
        {
            var result = _lectureService.GetLectureIdAsync(id);
            return result != null;
        }
    }
}
