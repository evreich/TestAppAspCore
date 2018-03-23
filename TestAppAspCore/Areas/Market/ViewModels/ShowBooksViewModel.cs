using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Areas.Market.ViewModels
{
    public class ShowBooksViewModel
    {
        public List<BookViewModel> Books { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string SearchExpr { get; set; } = string.Empty;
    }
}
