using System;

namespace WebApplication.Models.Dto
{
    public class OverviewDto
    {
        public string FacultyName { get; set; }
        public decimal? Financing { get; set; }
        public string DepartmentName { get; set; }
        public string GroupName { get; set; }
        public int? Rating { get; set; }
        public DateTime? Year { get; set; }
        public int? DayOfWeek { get; set; }
        public string LectureRoom { get; set; }
        public int? TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public string SubjectName { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public decimal? Premium { get; set; }
        public decimal? Salary { get; set; }
    }
}