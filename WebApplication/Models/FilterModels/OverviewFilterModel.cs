using System;

namespace WebApplication.Models.FilterModels
{
    public class OverviewFilterModel : PagableModel
    {
        public string Name { get; set; }
        public decimal? Financing { get; set; }
        public string TeacherSurname { get; set; }
        public string SubjectName { get; set; }
        public DateTime? EmploymentDate { get; set; }
    }
}