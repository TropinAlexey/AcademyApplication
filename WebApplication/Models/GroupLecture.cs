namespace WebApplication.Models
{
    public class GroupLecture
    {
        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        public int GroupId { get; set; }
        public int? LectureId { get; set; }
    }
}