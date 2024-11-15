namespace Domain.Contracts.Responses.Driver
{
    public record DriverResponseGetById(
        Guid Id,
        string Name,
        string NumberDocuments,
        string Description,
        float Forfeit,
        int CountForfeit);
}