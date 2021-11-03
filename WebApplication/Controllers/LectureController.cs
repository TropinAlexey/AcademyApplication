using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Models;
using WebApplication.Services.Interfaces;

namespace WebApplication.Controllers
{
    [Route("Lecture")]
    public class LectureController : Controller
    {
        private readonly ILectureService _lectureService;

        public LectureController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        // GET: Lectures
        [HttpGet("/Index")]
        public async Task<IActionResult> Index()
        {
            var result = await _lectureService.GetManyLecturesAsync();
            return View(result);
        }

        // GET: Lectures
        [HttpGet]
        public async Task<IActionResult> ShowSearchForm(string Search)
        {
            return View("Index", await _lectureService.SearchAsync(Search));
        }

        // GET: Lectures/Search
        [HttpGet("Search")]
        public IActionResult LecturesSearch()
        {
            return View("ShowSearchForm");
        }

        // GET: Lectures/Details/5
        [HttpGet("Details")]
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

        // GET: Lecture/Create
        [HttpGet("Create")]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewBag.Subject = new SelectList(await _lectureService.GetSubjectsAsync(), "Id", "Name");
            ViewBag.Teacher = new SelectList(await _lectureService.GetTeachersAsync(), "Id", "Name");
            ViewBag.Group = new SelectList(await _lectureService.GetGroupsAsync(), "Id", "Name");
            return View();
        }

        // POST: Lecture/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,LectureRoom,SubjectId,TeacherId,GroupId,DayOfWeek")] Lecture @entity)
        {
            if (ModelState.IsValid)
            {
                await _lectureService.CreateLectureAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        // GET: Lectures/Edit/5
        [HttpGet("Edit")]
        [Authorize]
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

        // POST: Lecture/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit")]
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

        // GET: Lecture/Delete/5
        [HttpGet("delete")]
        [Authorize]
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

        // POST: LecturesDelete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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
