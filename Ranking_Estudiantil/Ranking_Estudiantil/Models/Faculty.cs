using System.ComponentModel.DataAnnotations;

namespace Ranking_Estudiantil.Models
{
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Faculty Name")]
        public string FacultyName { get; set; } = null!;
        public byte Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public ICollection<Department>? Departments { get; set; }
    }
}
