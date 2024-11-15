using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Requests.Vehicle
{
    public record VehicleRequestUpdate(
        Guid Id,
        [Required(ErrorMessage = "Это поле обязательно")] 
        string Model,
        [Required(ErrorMessage = "Это поле обязательно")]
        [StringLength(Domain.Models.Vehicle.CountOfNum, MinimumLength = Models.Vehicle.CountOfNum, ErrorMessage = "Номер машины должен содержать шесть символов")]
        string CarNumbers,
        string? Description,
        Guid DriverId);
}
