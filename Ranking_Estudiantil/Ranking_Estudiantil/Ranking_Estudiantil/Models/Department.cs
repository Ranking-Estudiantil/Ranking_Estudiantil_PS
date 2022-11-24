using System.ComponentModel.DataAnnotations;

namespace Ranking_Estudiantil.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "Debe llenar este campo")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; } = null!;
        [Required(ErrorMessage = "Debe seleccionar una facultad")]
        public int FacultyID { get; set; }
        public byte Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public Faculty? Faculty { get; set; }
        public ICollection<Career>? Careers { get; set; }
    }
}
