using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.Models
{
    public class Order
    {
        [BindNever]
        [Required]
        public string UserId { get; set; }

        [BindNever]
        public User User { get; set; }

        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public bool? IsSuccess { get; set; } = null;

        [BindRequired]
        [Required]
        public DateTime DateReturn { get; set; }

        [BindNever]
        public IList<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Введите адрес получения.")]
        [MinLength(3, ErrorMessage = "Ошибка! Длина строки должна быть от 3 символов")]
        [RegularExpression(@"[а-яА-Я\w]+", ErrorMessage = "Ошибка! Некорректный формат страны.")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Введите область")]
        [MinLength(5, ErrorMessage = "Ошибка! Длина строки должна быть от 5 символов")]
        [RegularExpression(@"[а-яА-Я\w]+", ErrorMessage = "Ошибка! Некорректный формат области.")]
        [Display(Name = "Область")]
        public string State { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Введите город")]
        [MinLength(2, ErrorMessage = "Ошибка! Длина строки должна быть от 2 символов")]
        [RegularExpression(@"[а-яА-Я\w]+", ErrorMessage = "Ошибка! Некорректный формат города.")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Введите улицу")]
        [MinLength(5, ErrorMessage = "Ошибка! Длина строки должна быть от 5 символов")]
        [RegularExpression(@"[а-яА-Я\w\s]+", ErrorMessage = "Ошибка! Некорректный формат улицы.")]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Введите улицу")]
        [Range(1,500,ErrorMessage = "Ошибка. Введен отрицательный номер")]
        [Display(Name = "Дом")]
        public int HomeNumber { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Введите индекс")]
        [RegularExpression(@"\d{6}", ErrorMessage = "Ошибка! Некорректный формат.")]
        [Display(Name = "Почтовый индекс")]
        public string PostIndex { get; set; }

        [Display(Name = "Подарочная упаковка")]
        public bool GiftWrap { get; set; }
    }
}
