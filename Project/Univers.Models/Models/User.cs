using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Univers.Models.Models;

public class User
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете потребителско име.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете парола.")]
    public string? Password { get; set; }

    public string? PasswordSalt { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfRegistration { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public string? Image { get; set; }

    public bool? IsActive { get; set; }
}
