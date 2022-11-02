using System;
using System.Collections.Generic;

namespace Ranking_Estudiantil.Models
{
    public partial class Achievment
    {
        public Achievment()
        {
            IdStudents = new HashSet<Student>();
        }

        public short Id { get; set; }
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;

        public virtual ICollection<Student> IdStudents { get; set; }
    }
}
