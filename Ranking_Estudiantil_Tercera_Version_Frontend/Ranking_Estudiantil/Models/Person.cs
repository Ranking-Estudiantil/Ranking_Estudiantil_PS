using System.ComponentModel.DataAnnotations;

namespace Ranking_Estudiantil.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Second Last Name is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Second Last Name")]
        public string SecondLastName { get; set; }
        public byte Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}
