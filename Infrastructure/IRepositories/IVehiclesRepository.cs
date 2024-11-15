using Domain.Models;

namespace Infrastructure.IRepositories
{
    public interface IVehiclesRepository
    {
        IQueryable<Vehicle> GetAll();
        Task<Vehicle?> GetById(Guid id);
        Task CreateAsync(Vehicle entity);
        Task UpdateAsync(Vehicle entity);
        Task DeleteAsync(Vehicle entity);
    }
}