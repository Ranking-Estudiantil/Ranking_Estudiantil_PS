using System.ComponentModel.DataAnnotations;

namespace Ranking_Estudiantil.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; } = null!;
        public int FacultyID { get; set; }
        public Faculty? Faculty { get; set; }
        public ICollection<Career>? Careers { get; set; }
    }
}
