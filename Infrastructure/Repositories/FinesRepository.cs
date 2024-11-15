using Domain.Models;
using Infrastructure.Data;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FinesRepository : IFinesRepository
    {
        private readonly GosAutoDbContext _context;
        public FinesRepository(GosAutoDbContext context) 
        { 
            _context = context;
        }

        public async Task CreateAsync(Fine entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Fine entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Fine> GetAll()
        {
            return _context.Fines.AsNoTracking().Include(d => d.Driver);
        }

        public async Task <Fine?> GetById(Guid id)
        {
            return await _context.Fines.AsNoTracking().Include(v => v.Driver)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Fine entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
