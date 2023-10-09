using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class ChangeUsernameModel
    {
        public string? StudentId { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете потребителско име.")]
        [Display(Name = "Потребителско име")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Потребителското име трябва да е между 6 и 30 символа.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете потребителско име.")]
        [Display(Name = "Ново потребителско име")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Потребителското име трябва да е между 6 и 30 символа.")]
        public string? NewUsername { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете потребителско име.")]
        [Display(Name = "Повторете потребителското име")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Потребителското име трябва да е между 6 и 30 символа.")]
        [Compare("NewUsername", ErrorMessage = "Новите потребителски имена не съвпадат.")]
        public string? NewUsernameConfirmation { get; set; } 
    }
}
