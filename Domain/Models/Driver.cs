using Domain.Interfaces;

namespace Domain.Models
{
    public class Driver
    {
        public const int CountOfNum = 8;

        public Guid Id { get; }
        public string Name { get; set; }
        public string NumberDocuments { get; set; }
        public string? Description { get; set; }
        public float Forfeit { get; set; }
        public int CountForfeit { get; set; }

        public Driver(string name, string numberDocuments, string description, float forfeit = 0, int countForfeit = 0)
        {
            Id = Guid.NewGuid();
            Name = name;
            NumberDocuments = numberDocuments;
            Description = description;
            Forfeit = forfeit;
            CountForfeit = countForfeit;
        }

        public List<Vehicle> Vehicles { get; set; } = [];
        public List<Fine> Fines { get; set;  } = [];

    }
}
