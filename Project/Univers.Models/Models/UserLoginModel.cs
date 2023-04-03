using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Необходимо е да въведете потребителско име.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете парола.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Паролата трябва да е между 8 и 40 символа.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+[\]{};':""\\|,.<>/?])[a-zA-Z\d!@#$%^&*()_+[\]{};':""\\|,.<>/?]{8,}$", ErrorMessage = "Паролата трябва да съдържа поне една малка и главна буква, една цифра, един символ и да е дълга поне 8 знака.")]
        public string? Password { get; set; }
    }
}
