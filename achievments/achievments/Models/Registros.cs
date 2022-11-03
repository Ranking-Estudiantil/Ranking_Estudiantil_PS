namespace achievments.Models
{
    public class Registros
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int achievmentID { get; set; }
        public Achievments? Achievments { get; set; }
    }
}
