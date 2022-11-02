using System;
using System.Collections.Generic;

namespace Ranking_Estudiantil.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? SecondLastName { get; set; }
        public string Status { get; set; } = null!;
        public DateTime RegistreDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string Email { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public string Role { get; set; } = null!;

        public virtual Professor? Professor { get; set; }
        public virtual Student? Student { get; set; }
    }
}
