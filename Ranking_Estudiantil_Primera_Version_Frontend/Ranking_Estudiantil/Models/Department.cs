using System;
using System.Collections.Generic;

namespace Ranking_Estudiantil.Models
{
    public partial class Department
    {
        public Department()
        {
            Careers = new HashSet<Career>();
        }

        public byte Id { get; set; }
        public string Name { get; set; } = null!;
        public byte IdFaculty { get; set; }

        public virtual Faculty IdFacultyNavigation { get; set; } = null!;
        public virtual ICollection<Career> Careers { get; set; }
    }
}
