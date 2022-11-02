using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ranking_Estudiantil.Models
{
    public class Professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonID { get; set; }
        public int CareerID { get; set; }
        public Career? career { get; set; }
        public Person? Person { get; set; }
    }
}
