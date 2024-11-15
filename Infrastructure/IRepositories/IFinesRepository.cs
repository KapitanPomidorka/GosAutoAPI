using Domain.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IFinesRepository
    {
        IQueryable<Fine> GetAll();
        Task <Fine?> GetById(Guid Id);
        Task CreateAsync(Fine entity);
        Task UpdateAsync(Fine entity);
        Task DeleteAsync(Fine entity);
    }
}
