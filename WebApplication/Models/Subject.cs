using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Lecture> Lectures { get; set; }
    }
}