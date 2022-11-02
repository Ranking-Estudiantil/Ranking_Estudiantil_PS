using System;
using System.Collections.Generic;

namespace Ranking_Estudiantil.Models
{
    public partial class AcademicUnity
    {
        public AcademicUnity()
        {
            Faculties = new HashSet<Faculty>();
        }

        public byte Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
