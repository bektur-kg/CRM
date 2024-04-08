namespace CRM.Api.Constants;

public static class UserRoleMatches
{
    public const string AdminOrMarketer = $"{Admin}, {Marketer}";
    public const string MarketerOrSeller = $"{Marketer}, {Seller}";
    public const string Seller = nameof(UserRole.Seller);
    public const string Marketer = nameof(UserRole.Marketer);
    public const string Admin = nameof(UserRole.Admin);
}
