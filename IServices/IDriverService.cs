using Domain.Contracts.Requests.Driver;
using Domain.Contracts.Responses.Driver;
using Domain.Models;

namespace GosAutoAPI.IServices
{
    public interface IDriverService
    {
        IEnumerable<DriverResponseGetAll>? GetAllDrivers();
        Task <DriverResponseGetById?> GetDriverById(Guid id);
        Task CreateDriver(DriverRequestCreate request);
        Task UpdateDriver(DriverRequestUpdate request);
        Task DeleteDriver(DriverRequestDelete request);
        IEnumerable<string> GetListDrivers();
        IEnumerable<Guid> GetDriverId();
        Task UpdateAfterFineUpdate(Guid DriverId);
    }
}