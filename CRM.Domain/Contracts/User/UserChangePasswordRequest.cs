namespace CRM.Domain.Contracts.User;

public record UserChangePasswordRequest
{
    public required string OldPassword { get; set; }

    public required string NewPassword { get; set; }
}
