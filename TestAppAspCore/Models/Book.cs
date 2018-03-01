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
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreating { get; set; }

        [Required]
        [RegularExpression("\\w+ \\w+\\s?.*")]
        public string Author { get; set; }
        
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
