using Domain.Models;
using Infrastructure.Data;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly GosAutoDbContext _context;
        public UsersRepository(GosAutoDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.AsNoTracking();
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByNameAsync(string username)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task UpdatePasswordAsync(Guid id, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            user.Password = password;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public Task UpdatePasswordAsync(User entity, string password)
        {
            throw new NotImplementedException();
        }
    }
}
