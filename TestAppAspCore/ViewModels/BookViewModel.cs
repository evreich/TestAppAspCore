using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace TestAppAspCore.ViewModels
{

    public class BookViewModel
    {       
        public int Id { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Пустое значение.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Ошибка! Длина строки должна быть от 1 до 50 символов.")]
        [Display(Name = "Наименование")]
        public string Title { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Пустое значение.")]
        [Display(Name = "Дата создания")]
        public DateTime DateCreating { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Пустое значение.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Ошибка! Длина строки должна быть от 1 до 50 символов.")]
        [RegularExpression(@"[а-яА-Я\w]+ [а-яА-Я\w]+\s?.*", ErrorMessage = "Ошибка! Некорректный формат ФИ автора.")]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Пустое значение.")]
        [RegularExpression(@"\d+",ErrorMessage = "Ошибка! Жанр не выбран!")]
        [Display(Name = "Жанр")]
        public int GenreId { get; set; }

        [Display(Name = "Жанр")]
        public string GenreTitle { get; set; }

        [Display(Name = "Количество")]
        [BindRequired]
        [Range(1, 50, ErrorMessage = "Ошибка! Введено неверное количество книг")]
        [Required(ErrorMessage = "Ошибка! Пустое значение.")]
        public int Count { get; set; }
    }
}
