using System;
using System.Collections.Generic;

namespace Ranking_Estudiantil.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Departments = new HashSet<Department>();
        }

        public byte Id { get; set; }
        public string Name { get; set; } = null!;
        public byte IdAcademicUnity { get; set; }

        public virtual AcademicUnity IdAcademicUnityNavigation { get; set; } = null!;
        public virtual ICollection<Department> Departments { get; set; }
    }
}
