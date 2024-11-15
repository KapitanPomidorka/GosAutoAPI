using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public record AuthViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно")]
        public string? UserName;

        [Required(ErrorMessage = "Это поле обязательно")]
        public string? Password;

        public bool RememberMe;

    }
}

