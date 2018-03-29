using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Areas.Market.ViewModels
{
    public class ShowBooksViewModel : IndexBookViewModel
    {
        public string Genre { get; set; } = string.Empty;
        public string ActionName { get; set; }
    }
}
