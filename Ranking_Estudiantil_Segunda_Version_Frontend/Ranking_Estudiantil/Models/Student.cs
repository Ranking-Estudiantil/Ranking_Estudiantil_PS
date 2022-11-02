namespace Ranking_Estudiantil.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public byte Rank { get; set; }
        public short Score { get; set; }
        public int CareerID { get; set; }
        public Career? career { get; set; }
    }
}
