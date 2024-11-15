using Infrastructure.Data;
using Infrastructure.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly GosAutoDbContext _context;

        public VehiclesRepository(GosAutoDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Vehicle entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Vehicle entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Vehicle> GetAll()
        {
            return _context.Vehicles.AsNoTracking().Include(d => d.Driver); 
        }

        public async Task<Vehicle?> GetById(Guid id)
        {
            return await _context.Vehicles.AsNoTracking().Include(v => v.Driver)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Vehicle entity)
        {
            _context.Vehicles.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
