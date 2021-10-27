using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication.Models;
using WebApplication.Models.Dto;
using WebApplication.Services.Interfaces;

namespace WebApplication.Data.Repositories
{
    public class FacultiesRepository : BaseRepository<Faculty>, IFacultiesRepository
    {
        private readonly ApplicationDbContext _context;

        public FacultiesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<OverviewDto> GetOverview()
        {
            var result = (from f in _context.Faculties
                          from d in _context.Departments.Where(d => d.FacultyId == f.Id)
                          from g in _context.Groups.Where(g => g.DepartmentId == d.Id)
                          from gr in _context.GroupsLectures.Where(gr => gr.GroupId == g.Id).DefaultIfEmpty()
                          from l in _context.Lectures.Where(l => l.Id == gr.LectureId.Value).DefaultIfEmpty()
                          from t in _context.Teachers.Where(t => t.Id == l.TeacherId).DefaultIfEmpty()
                          from s in _context.Subjects.Where(s => s.Id == l.SubjectId).DefaultIfEmpty()
                          select new OverviewDto
                          {
                              Financing = d.Financing,
                              DepartmentName = d.Name,
                              FacultyName = f.Name,
                              GroupName = g.Name,
                              Rating = g.Rating,
                              Year = g.Year,
                              DayOfWeek = gr.DayOfWeek,
                              LectureRoom = l.LectureRoom,
                              SubjectName = s.Name,
                              EmploymentDate = t.EmploymentDate,
                              TeacherName = t.Name,
                              Premium = t.Premium,
                              Salary = t.Salary,
                              TeacherSurname = t.Surname
                          }).AsNoTracking();
            return result;
        }
    }
}