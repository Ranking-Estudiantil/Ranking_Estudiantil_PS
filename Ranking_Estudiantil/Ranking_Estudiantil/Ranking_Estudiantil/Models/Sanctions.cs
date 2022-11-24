using System.ComponentModel.DataAnnotations;

namespace Ranking_Estudiantil.Models
{
    public class Sanctions
    {
        [Key]
        public int SanctionsID { get; set; }
       
        public int StudentsID { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(150, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 15)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Debe ingresar la cantidad de puntaje")]
        public double punctuation { get; set; }
        public Student? Student { get; set; }
    }
}
