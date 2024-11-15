using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Requests.Fine
{
    public record FineRequestCreate
        (
            [Required(ErrorMessage = "Это обязательное поле")]string Description,
            [Required(ErrorMessage = "Это обязательное поле")]
            [Range(0, 10000, ErrorMessage = "Штраф не может быть меньше нуля или больше десяти тысячей")]
            float Fines,
            Guid DriverId,
            bool IsPaid
        );

}
