namespace Domain.Models
{
    public class Vehicle
    {
        public const int CountOfNum = 6;
        public Guid Id { get; }
        public string Model { get; set; }
        public string CarNumbers { get; set; }
        public string? Description { get; set; }

        public Guid DriverId { get; set; }
        public Driver? Driver { get; set; }

        public Vehicle(string model, string carNumbers, string description, Guid driverId)
        {
            Id = new Guid();
            Model = model;
            CarNumbers = carNumbers;
            Description = description;
            DriverId = driverId;
        }
    }
}
