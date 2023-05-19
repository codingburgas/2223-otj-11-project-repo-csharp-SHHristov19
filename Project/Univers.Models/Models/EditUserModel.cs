using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class EditUserModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете потребителско име.")]
        [Display(Name = "Потребителско име")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Потребителското име трябва да е между 6 и 30 символа.")]
        public string? Username { get; set; } = null!; 

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
    }
}
