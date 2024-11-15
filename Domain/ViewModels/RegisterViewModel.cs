using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels
{
    public record RegisterViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно")]
        public string? UserName;

        [Required(ErrorMessage = "Это поле обязательно")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{10,}$",
                ErrorMessage = "Пароль слишком простой")]
        public string? Password {  get; init; }

        //public User ToUser()
        //{
            //return new User()
            //{
            //    UserName = this.UserName!,
            //    Password = this.Password!
            //};
        //}
    }
}
