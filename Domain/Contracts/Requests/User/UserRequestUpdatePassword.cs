using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Requests.User
{
    public record UserRequestUpdatePassword(
        Guid Id,
        string Password,
        string salt
        );
}
