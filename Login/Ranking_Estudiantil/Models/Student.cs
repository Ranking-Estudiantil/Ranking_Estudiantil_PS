using System;
using System.Collections.Generic;

namespace Ranking_Estudiantil.Models
{
    public partial class Student
    {
        public Student()
        {
            IdAchievments = new HashSet<Achievment>();
        }

        public int Id { get; set; }
        public short Score { get; set; }
        public byte IdCareer { get; set; }
        public byte IdRank { get; set; }

        public virtual Career IdCareerNavigation { get; set; } = null!;
        public virtual Person IdNavigation { get; set; } = null!;
        public virtual Rank IdRankNavigation { get; set; } = null!;

        public virtual ICollection<Achievment> IdAchievments { get; set; }
    }
}
