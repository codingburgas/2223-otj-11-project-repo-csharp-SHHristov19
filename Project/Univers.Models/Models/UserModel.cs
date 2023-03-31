using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Univers.Models.Models;

public class UserModel 
{
    [Key]
    public string? Id { get; set; }

    [Display(Name = "Въведете потребителско име.")]
    [Required(ErrorMessage = "Необходимо е да въведете потребителско име.")]
    [MaxLength(30)]
    public string? Username { get; set; }

    [Display(Name = "Въведете парола.")]
    [Required(ErrorMessage = "Необходимо е да въведете парола.")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "Паролата трябва да е между 8 и 40 символа.")]
    [DataType(DataType.Password, ErrorMessage = "Паролата трябва да съдържа поне една малка буква, една главна буква, една цифра и да е дълга поне 8 знака.")]
    public string? Password { get; set; }

    [Display(Name = "Въведете повторн паролата.")]
    [Required(ErrorMessage = "Необходимо е да въведете парола.")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "Паролата трябва да е между 8 и 40 символа.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Паролата трябва да съдържа поне една малка буква, една главна буква, една цифра и да е дълга поне 8 знака.")]
    [Compare("Password", ErrorMessage = "Паролата не съвпада.")]
    public string? PasswordConfirmation { get; set; }

    public string? PasswordSalt { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfRegistration { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Phone]
    public string? PhoneNumber { get; set; } = " ";

    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public string? Gender { get; set; }

    public string? Image { get; set; }

    public bool? IsActive { get; set; }

    public string? RoleChoice { get; set; }
}
