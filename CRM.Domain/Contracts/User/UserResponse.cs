﻿using CRM.Domain.Enums;

namespace CRM.Domain.Contracts.User;

public record UserResponse
{
    public required long Id { get; set; }

    public string? FullName { get; set; }

    public required string Email { get; set; }

    public required UserRole Role { get; set; }

    public DateTime? BlockDate { get; set; }
}
