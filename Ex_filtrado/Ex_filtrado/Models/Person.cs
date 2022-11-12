using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ex_filtrado.Models
{
    public class Person
    {

        [Key]
        public int PersonID { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Rango { get; set; }
        public string Carrera { get; set; }
    }
}
