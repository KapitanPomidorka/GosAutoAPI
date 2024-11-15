using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IUsersRepository
    {
        Task CreateAsync(User entity);
        Task<User?> GetById(Guid id);
        IEnumerable<User> GetAll();
        Task<User?> GetByNameAsync(string username);
        Task UpdatePasswordAsync(User entity, string password);
        Task DeleteAsync(User entity);  //Реализуем в другом интерфейсе для администрирования
    }
}
