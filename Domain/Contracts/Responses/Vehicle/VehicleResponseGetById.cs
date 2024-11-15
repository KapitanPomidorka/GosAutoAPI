namespace Domain.Contracts.Responses.Vehicle
{
    public record VehicleResponseGetById(
        Guid Id,
        string Model,
        string CarNumbers,
        string Description,
        Guid DriverId,
        Models.Driver Driver);
}
