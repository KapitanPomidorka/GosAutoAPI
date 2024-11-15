using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public int? Status { get; set; }
        public DateTime? LastSession { get; set; }

        public User(string username, string password, string salt, int status = 1)
        {
            Id = Guid.NewGuid();
            UserName = username;
            Password = password;
            Salt = salt;
            Status = status;
            LastSession = DateTime.Now;
        }
    }
}
