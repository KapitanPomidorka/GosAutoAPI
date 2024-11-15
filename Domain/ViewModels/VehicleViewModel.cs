using Domain.Models;

namespace Domain.ViewModels
{
    public record VehicleViewModel(
     Guid Id,
     string Model,
     string CarNumbers,
     string Description,
     Guid DriverId,
     IEnumerable<Driver> Drivers);
}
