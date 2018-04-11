using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.Areas.Market.ViewModels
{
    public class StoreBooksViewModel
    {
        public List<Book> StoreBooks { get; set; }
        public List<Book> NotConfirmedBooks { get; set; }
    }
}
