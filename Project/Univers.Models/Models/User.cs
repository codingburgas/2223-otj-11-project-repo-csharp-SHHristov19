using System;
using System.Collections.Generic;

namespace UniversModels.Models;

public class User
{
    public string? Id { get; set; } = null!;

    public string? Username { get; set; } = null!;

    public string? Password { get; set; } = null!;

    public string? PasswordSalt { get; set; } = null!;

    public string? FirstName { get; set; } = null!;

    public string? MiddleName { get; set; } = null!;

    public string? LastName { get; set; } = null!;

    public DateTime? DateOfRegistration { get; set; } = null!;

    public string? PhoneNumber { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public string? Address { get; set; } = null!;

    public string? Gender { get; set; } = null!;

    public string? Image { get; set; } = null!;

    public bool? IsActive { get; set; } = null!;
}
