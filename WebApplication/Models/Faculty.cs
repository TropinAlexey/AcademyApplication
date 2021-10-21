using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
    }
}