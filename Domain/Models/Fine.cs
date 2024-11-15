using Domain.Interfaces;

namespace Domain.Models
{
    public class Fine
    {
        public Guid Id { get; }
        public string Description { get; set; }
        public float Fines { get; set; }
        public bool IsPaid { get; set; } = false;

        public Guid DriverId { get; set; }
        public Driver? Driver { get; set; }

        public static Dictionary<Guid, Guid> BondBetween = [];

        public Fine(string description, float fines, Guid driverId, bool isPaid)
        {
            Id = Guid.NewGuid();
            Description = description;
            Fines = fines;
            DriverId = driverId;
            IsPaid = isPaid;
        }

    }
}
