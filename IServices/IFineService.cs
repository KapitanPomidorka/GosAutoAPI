using Domain.Contracts.Requests.Driver;
using Domain.Contracts.Requests.Fine;
using Domain.Contracts.Responses.Fine;
using Domain.Models;

namespace GosAutoAPI.IServices
{
    public interface IFineService
    {
        Task CreateFine(FineRequestCreate request);
        Task UpdateFine(FineRequestUpdate request);
        Task DeleteFine(FineRequestDelete request);
        IEnumerable<FineResponseGetAll> GetAllFines();
        Task<FineResponseGetById?> GetFineById(Guid guid);
    }
}
