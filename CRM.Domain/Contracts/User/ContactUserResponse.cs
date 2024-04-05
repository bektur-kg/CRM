namespace CRM.Domain.Contracts.User;

public record ContactUserResponse
{
    public required long Id { get; set; }

    public string? FullName { get; set; }

    public required string Email { get; set; }
}
