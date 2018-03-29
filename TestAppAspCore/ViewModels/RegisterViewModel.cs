using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^[\w\_\.\-]+@[\w\-\_]+\.\w{2,3}$", ErrorMessage = "Ошибка! Некорректный формат email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        [RegularExpression(@"[а-яА-Я\w]+ [а-яА-Я\w]+ [а-яА-Я\w]+$", ErrorMessage = "Ошибка! Некорректный формат ФИО")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Ошибка! Некорректный формат телефона")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Ошибка! Длина ключа должна быть >= 5")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
