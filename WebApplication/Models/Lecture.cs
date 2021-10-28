using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string LectureRoom { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        [NotMapped]
        public int GroupId { get; set; }
        [NotMapped]
        [Range(1, 7)]
        public int DayOfWeek { get; set; }        
        
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}