using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Requests.Driver
{
    public record DriverRequestUpdate(
        Guid Id,
        [Required(ErrorMessage = "Это поле обязательно")]
        string Name,
        [Required(ErrorMessage = "Это поле обязательно")]
        [StringLength(Models.Driver.CountOfNum, MinimumLength = Models.Driver.CountOfNum,  ErrorMessage = "Номер документа должен содержать восемь цифр")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Номер документа должен содержать только цифры")]
        string NumberDocuments,
        string? Description,
        float Forfeit,
        int CountForfeit);
}