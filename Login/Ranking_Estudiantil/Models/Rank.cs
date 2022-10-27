using System;
using System.Collections.Generic;

namespace Ranking_Estudiantil.Models
{
    public partial class Rank
    {
        public Rank()
        {
            Students = new HashSet<Student>();
        }

        public byte Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}
