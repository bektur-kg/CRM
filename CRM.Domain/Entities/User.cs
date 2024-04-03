﻿using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities;

public class User
{
    public required long Id { get; set; }

    [StringLength(300)]
    public string? FullName { get; set; }

    [StringLength(200)]
    public required string Email { get; set; }

    //todo: add string length 
    public required string PasswordHash { get; set; }

    public required UserRole Role { get; set; }

    public DateTime? BlockDate { get; set; }
}
