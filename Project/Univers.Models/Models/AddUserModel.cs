using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Univers.Models.Models
{
    public class AddUserModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете потребителско име.")]
        [Display(Name = "Потребителско име")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Потребителското име трябва да е между 6 и 30 символа.")]
        public string? Username { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете парола.")]
        [Display(Name = "Парола")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Паролата трябва да е между 8 и 40 символа.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+[\]{};':""\\|,.<>/?])[a-zA-Z\d!@#$%^&*()_+[\]{};':""\\|,.<>/?]{8,}$", ErrorMessage = "Паролата трябва да съдържа поне една малка и главна буква, една цифра, един символ и да е дълга поне 8 знака.")]
        public string? Password { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете повторно паролата.")]
        [Display(Name = "Повторна парола")]
        [Compare("Password", ErrorMessage = "Паролата не съвпада.")]
        public string? PasswordConfirmation { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете име.")]
        [Display(Name = "Име")]
        [RegularExpression(@"^[A-Za-zА-Яа-я]+$", ErrorMessage = "Името трябва да съдържа само букви!")]
        public string? FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете презиме.")]
        [Display(Name = "Презиме")]
        [RegularExpression(@"^[A-Za-zА-Яа-я]+$", ErrorMessage = "Името трябва да съдържа само букви!")]
        public string? MiddleName { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете фамилия.")]
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[A-Za-zА-Яа-я]+$", ErrorMessage = "Името трябва да съдържа само букви!")]
        public string? LastName { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете телефонен номер.")]
        [Display(Name = "Телефонен номер")]
        [RegularExpression(@"^\+359\d{9}$|^\0\d{9}$", ErrorMessage = "Невалиден телефонен номер")]
        public string? PhoneNumber { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Неправилен email адрес.")]
        [Display(Name = "Имейл адрес")]
        [Required(ErrorMessage = "Необходимо е да въведете email.")]
        public string? Email { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете адрес.")]
        [Display(Name = "Адрес")]
        public string? Address { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете пол.")]
        [Display(Name = "Пол")]
        public string? Gender { get; set; } = null!;

        public StaffModel? Staff { get; set; } = null!;
    }
}
