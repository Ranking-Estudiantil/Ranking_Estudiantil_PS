using System;
using System.Collections.Generic;

namespace Ranking_Estudiantil.Models
{
    public partial class Professor
    {
        public int Id { get; set; }
        public byte IdCareer { get; set; }

        public virtual Career IdCareerNavigation { get; set; } = null!;
        public virtual Person IdNavigation { get; set; } = null!;
    }
}
