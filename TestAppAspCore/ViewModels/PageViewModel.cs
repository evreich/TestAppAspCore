using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.ViewModels
{
    public class PageViewModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public string SearchExpr { get; }

        public PageViewModel(int count, int pageNumber, int pageSize, string searchExpr)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SearchExpr = searchExpr;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
