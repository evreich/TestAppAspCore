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
        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        public List<Book> Books { get; set; }
    }
}
