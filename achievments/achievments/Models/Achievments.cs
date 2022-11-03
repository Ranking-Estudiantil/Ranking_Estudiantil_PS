namespace achievments.Models
{
    public class Achievments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public ICollection<Registros>? Registros { get; set; }
    }
}
