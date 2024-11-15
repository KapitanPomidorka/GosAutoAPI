using Domain.Contracts.Requests.Fine;
using Domain.Models;

namespace Infrastructure.IRepositories
{
    public interface IDriversRepository
    {
        IEnumerable<Driver> GetAll();
        Task <Driver?> GetById(Guid id);
        Task CreateAsync(Driver entity);
        Task UpdateAsync(Driver entity);
        Task DeleteAsync(Driver entity);

    }
}
