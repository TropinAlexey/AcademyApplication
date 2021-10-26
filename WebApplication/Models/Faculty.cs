using System.ComponentModel;

namespace WebApplication.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
    }
}