using Infrastructure.Data;
using Infrastructure.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DriversRepository : IDriversRepository

    {
        private readonly GosAutoDbContext _context;

        public DriversRepository(GosAutoDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Driver entity)
        {
            await _context.DriversTable.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Driver entity)
        {
            _context.DriversTable.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Driver> GetAll()
        {
            return _context.DriversTable.AsNoTracking();
        }

        public async Task <Driver?> GetById(Guid id)
        {
            return await _context.DriversTable.FirstOrDefaultAsync(d => d.Id == id);
        }


        public async Task UpdateAsync(Driver entity)
        {
            _context.DriversTable.Update(entity);
            await _context.SaveChangesAsync();

        }

        
    }
}
