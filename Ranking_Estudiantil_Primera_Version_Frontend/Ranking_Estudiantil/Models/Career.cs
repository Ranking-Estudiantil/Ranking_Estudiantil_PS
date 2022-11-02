using System;
using System.Collections.Generic;

namespace Ranking_Estudiantil.Models
{
    public partial class Career
    {
        public Career()
        {
            Professors = new HashSet<Professor>();
            Students = new HashSet<Student>();
        }

        public byte Id { get; set; }
        public string Name { get; set; } = null!;
        public byte IdDepartment { get; set; }

        public virtual Department IdDepartmentNavigation { get; set; } = null!;
        public virtual ICollection<Professor> Professors { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
