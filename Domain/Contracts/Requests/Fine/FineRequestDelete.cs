using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Requests.Fine
{
    public record FineRequestDelete(Guid Id, Guid DriverId);
}
