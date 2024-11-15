using Domain.Contracts.Requests.User;
using Domain.Models;
using GosAutoAPI.IServices.Auth;
using Infrastructure.IRepositories;
using System.ComponentModel.DataAnnotations;

namespace GosAutoAPI.Services.Auth
{
    public class Authentication : IAuthentication
    {
        private readonly IUsersRepository _repository;
        private readonly IEncrypt _encrypt;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Authentication(IUsersRepository repository, IEncrypt encrypt, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _encrypt = encrypt;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Authenticate(string username, string password, bool rememberme)
        {
            //Можно реализовать неудачные попытки
            User? user = await _repository.GetByNameAsync(username);
            if (user == null)
            {
                return false;
            }
            if (user.Password == _encrypt.HashPassword(password, user.Salt))
            {
                Login(user.Id, rememberme);
                return true;
            }
            return false;
        }

        public async Task CreateUser(UserRequestCreate request)
        {
            User? user = await _repository.GetByNameAsync(request.UserName);
            if (user == null)
            {
                user.UserName = request.UserName;
                user.Password = request.Password;
                user.Salt = request.Salt;
                user.Status = request.Status;
                user.LastSession = request.LastSession;

                await _repository.CreateAsync(user);
            }
            else
            {
                //Неудачная попытка (пользователь уже существует)
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            
            User? user = await _repository.GetById(id);
            if(user != null)
            {
                return user;
            }
            throw new NotImplementedException(); //Неудачная попытка (пользователь не найден)
        }

        public async Task<User> GetUserByName(string username)
        {
            User? user = await _repository.GetByNameAsync(username);
            if (user != null)
            {
                return user;
            }
            throw new NotImplementedException(); //Неудачная попытка (пользователь не найден)
        }

        public Task<bool> IsAccountLocked(string email)
        {
            throw new NotImplementedException(); //Защита от перебора
        }

        public void Login(Guid Id, bool rememberme)
        {
            _httpContextAccessor?.HttpContext?.Session.SetString("id", Id.ToString());
            if(rememberme)
            {
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTimeOffset.UtcNow.AddDays(30);
                cookie.Secure = true;
                cookie.HttpOnly = true;
                _httpContextAccessor?.HttpContext?.Response.Cookies.Append(General.Constants.RememberMeCookieName, _encrypt.Encrypt(Id.ToString()), cookie);

            }
        }

        public async Task UpdatePassword(UserRequestUpdatePassword request)
        {
            User? user = await _repository.GetById(request.Id);
            if(user == null)
            {
                throw new NotImplementedException(); //Неудачная попытка (пользователь не найден)
            }
            await _repository.UpdatePasswordAsync(user, _encrypt.HashPassword(request.Password, request.salt));
        }
    }
}
