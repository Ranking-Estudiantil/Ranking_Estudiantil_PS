using System.ComponentModel.DataAnnotations;

namespace Ranking_Estudiantil.Models
{
    public class AcademicUnity
    {
        [Key]
        public int AcademicUnityID { get; set; }
        [Required(ErrorMessage = "Debe llenar este campo")]
        [StringLength(50, ErrorMessage = "{0} debe ser minimo de {2} caracteres y maximo {1}", MinimumLength = 3)]
        [Display(Name = "Unidad Academica")]
        public string AcademicUnityName { get; set; } = null!;
        public byte Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public ICollection<Person>? People { get; set; }
       

    }
}
