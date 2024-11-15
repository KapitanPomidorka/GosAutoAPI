
namespace Domain.Contracts.Responses.Vehicle
{
    public record VehicleResponseGetAll(
        Guid Id,
        string Model,
        string CarNumbers,
        string Description,
        Guid DriverId,
        Models.Driver Driver);

}
