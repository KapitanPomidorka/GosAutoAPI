using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Responses.Driver
{
    public record DriverResponseGetAll(
        Guid Id,
        string Name,
        string NumberDocuments,
        string? Description,
        float Forfeit,
        int CountForfeit);
}