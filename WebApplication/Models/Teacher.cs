using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Premium { get; set; }
        [Range(0, int.MaxValue)]
        public decimal Salary { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
