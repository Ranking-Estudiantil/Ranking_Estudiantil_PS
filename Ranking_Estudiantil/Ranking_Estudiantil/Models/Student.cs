using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ranking_Estudiantil.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonID { get; set; }
        public byte Rank { get; set; }
        public int Score { get; set; }
        public Person? PeronStud { get; set; }
    }
}
