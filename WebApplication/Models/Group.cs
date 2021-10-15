using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Group
    {
        public Group()
        {
            Lectures = new List<Lecture>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1, 12)]
        public int Rating { get; set; }
        public DateTime Year { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}