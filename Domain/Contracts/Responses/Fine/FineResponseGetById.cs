using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Responses.Fine
{
    public record FineResponseGetById
        (
            Guid Id,
            string Description,
            float Fines,
            Guid DriverId,
            bool IsPaid,
            Models.Driver Driver
        );
}
