using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Univers.Models.Models;

public class UserModel 
{ 
    public string? Id { get; set; }
     
    public string? Username { get; set; }
     
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

    public string? Code { get; set; } = null!;
} 