using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.ViewModels
{
    public class IndexBookViewModel
    {
        public List<BookViewModel> Books { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string SearchExpr { get; set; } = string.Empty;
    }
}
