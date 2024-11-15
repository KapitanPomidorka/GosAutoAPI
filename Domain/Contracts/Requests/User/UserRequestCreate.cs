using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Requests.User
{
    public record UserRequestCreate(
        Guid Id,
        string UserName,
        string Password,
        string Salt,
        int Status,
        DateTime LastSession
        );
}
