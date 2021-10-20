using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("Faculty")]
    public class Department
    {
        public int Id { get; set; }

        [DisplayName("Финансирование, $")]
        [Range(0, int.MaxValue)]
        [Column(TypeName = "money")]
        public decimal Financing { get; set; }

        [Required]
        [DisplayName("Название")]
        [MaxLength(250)]
        public string Name { get; set; }

        [InverseProperty(nameof(Faculty.Id))]
        public int FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }
    }
}