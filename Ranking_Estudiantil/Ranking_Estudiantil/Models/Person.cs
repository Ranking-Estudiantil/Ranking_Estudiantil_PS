using System.ComponentModel.DataAnnotations;

namespace Ranking_Estudiantil.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        [Required(ErrorMessage = "Debe ingresar el nombre ")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Debe ingresar el apellido paterno")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
       
        public string? SecondLastName { get; set; }
        public int AcademicUnityID { get; set; }
        public int CareerID { get; set; }
        
        public byte Status { get; set; }
        public DateTime RegisterDate { get; set; }

        public string Role { get; set; }
        public string Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public Career? career { get; set; }
        public AcademicUnity? academicUnity { get; set; }
        public Professor? professor { get; set; }
        public Student? student { get; set; }
        
    }
}
