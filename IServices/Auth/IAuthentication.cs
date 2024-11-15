using Domain.Contracts.Requests.User;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace GosAutoAPI.IServices.Auth
{
    public interface IAuthentication
    {
        Task CreateUser(UserRequestCreate request);
        //Task<ValidationResult?> ValidateEmail(string? email); - пока реализация не нужна
        Task<bool> Authenticate(String username, String password, bool rememberme);
        Task<bool> IsAccountLocked(String username);
        Task UpdatePassword(UserRequestUpdatePassword request);
        void Login(Guid Id, bool rememberme);
    }
}
