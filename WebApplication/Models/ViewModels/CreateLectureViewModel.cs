namespace WebApplication.Models.ViewModels
{
    public class CreateLectureViewModel
    {
        public int Id { get; set; }
        public string LectureRoom { get; set; }
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }
        public string GroupName { get; set; }
        public int? DayOfWeek { get; set; }
    }
}