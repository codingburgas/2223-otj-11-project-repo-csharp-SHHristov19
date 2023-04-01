using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Univers.Models.Models;

public class UserModel 
{ 
    public string? Id { get; set; }
     
    [Required(ErrorMessage = "Необходимо е да въведете потребителско име.")]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "Потребителското име трябва да е между 6 и 30 символа.")] 
    public string? Username { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете потребителско име.")]
    public string UsernameLogin { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете парола.")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "Паролата трябва да е между 8 и 40 символа.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+[\]{};':""\\|,.<>/?])[a-zA-Z\d!@#$%^&*()_+[\]{};':""\\|,.<>/?]{8,}$", ErrorMessage = "Паролата трябва да съдържа поне една малка и главна буква, една цифра, един символ и да е дълга поне 8 знака.")]
    public string? Password { get; set; }
     
    [Required(ErrorMessage = "Необходимо е да въведете повторно паролата.")]
    [Compare("Password", ErrorMessage = "Паролата не съвпада.")]
    public string? PasswordConfirmation { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете парола.")]
    public string? PasswordLogin { get; set; }

    public string? PasswordSalt { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете име.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете презиме.")]
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете фамилия.")]
    public string? LastName { get; set; }

    public DateTime? DateOfRegistration { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете телефонен номер.")]
    [RegularExpression(@"^\+359\d{9}$|^\0\d{9}$", ErrorMessage = "Невалиден телефонен номер")]
    public string? PhoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "Неправилен email адрес.")]
    [Required(ErrorMessage = "Необходимо е да въведете email.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете адрес.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете пол.")]
    public string? Gender { get; set; }

    public string? Image { get; set; }

    public string? RoleChoice { get; set; }
} 