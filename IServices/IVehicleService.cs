using Domain.Contracts.Requests.Vehicle;
using Domain.Contracts.Responses.Vehicle;
using Domain.Models;
namespace GosAutoAPI.IServices
{
    public interface IVehicleService
    {
        Task CreateVehicle(VehicleRequestCreate request);
        Task DeleteVehicle(VehicleRequestDelete request);
        IEnumerable<VehicleResponseGetAll>? GetAllVehicles();
        Task UpdateVehicle(VehicleRequestUpdate request);
        Task<VehicleResponseGetById?> GetVehicleById(Guid Id);
    }
}