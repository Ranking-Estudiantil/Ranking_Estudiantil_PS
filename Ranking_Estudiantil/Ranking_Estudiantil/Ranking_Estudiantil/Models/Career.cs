using System.ComponentModel.DataAnnotations;

namespace Ranking_Estudiantil.Models
{
    public class Career
    {
        [Key]
        public int CareerID { get; set; }
        [Required(ErrorMessage = "Debe llenar este campo")]
        [StringLength(50, ErrorMessage = "{0} debe ser minimo de {2} caracteres y maximo {1}", MinimumLength = 3)]
        [Display(Name = "Career Name")]
        public string CareerName { get; set; } = null!;
        public int DepartmentID { get; set; }
        public Department? Department { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
