using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.Models
{
    public class Genre: DBModel
    {
        [BindRequired]
        [Required(ErrorMessage = "Ошибка! Пустое значение.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Ошибка! Длина строки должна быть от 3 до 20 символов.")]
        [Display(Name = "Наименование")]
        public string Title { get; set; }

        [JsonIgnore]
        public List<Book> Books { get; set; }

        public Genre()
        {
            Books = new List<Book>();
        }
    }
}
