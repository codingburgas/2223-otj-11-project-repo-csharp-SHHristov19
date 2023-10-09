using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class ChangePasswordModel
    {
        public string? StudentId { get; set;}

        [Required(ErrorMessage = "Необходимо е да въведете парола.")]
        [Display(Name = "Парола")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Паролата трябва да е между 8 и 40 символа.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете нова парола.")]
        [Display(Name = "Нова парола")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Новата паролата трябва да е между 8 и 40 символа.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+[\]{};':""\\|,.<>/?])[a-zA-Z\d!@#$%^&*()_+[\]{};':""\\|,.<>/?]{8,}$", ErrorMessage = "Паролата трябва да съдържа поне една малка и главна буква, една цифра, един символ и да е дълга поне 8 знака.")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете повторно новата паролата.")]
        [Display(Name = "Потвърди паролата")]
        [Compare("NewPassword", ErrorMessage = "Новата паролата не съвпада.")]
        public string? NewPasswordConfirmation { get; set; }
    }
}
