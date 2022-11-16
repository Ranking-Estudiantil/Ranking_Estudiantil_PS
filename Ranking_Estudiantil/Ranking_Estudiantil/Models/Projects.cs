using System.ComponentModel.DataAnnotations;

namespace Ranking_Estudiantil.Models
{
    public class Projects
    {
        [Key]
        public int ProjectsID { get; set; }
        
        public int StudentsID { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un tipo de logro")]
        public string achievment { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(150, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 15)]
        public string ProjectName { get; set; } = null!;
        [Required(ErrorMessage = "Debe ingresar un puntaje")]
        public double punctuation { get; set; }
        public byte Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public Student? Student { get; set; }
    }
}
