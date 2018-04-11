using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }

        [Display(Name = "Список книг:")]
        public BookOfOrder[] BooksOfOrder { get; set; }
    }

    public class BookOfOrder
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public bool Selected { get; set; } = true;
    }
}
