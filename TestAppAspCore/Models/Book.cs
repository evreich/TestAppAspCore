using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.Models
{
    public class Book : DBModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime DateCreating { get; set; }

        [Required]
        public string Author { get; set; }
        
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        [Required]
        public int Count { get; set; }

        public ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();
    }
}
