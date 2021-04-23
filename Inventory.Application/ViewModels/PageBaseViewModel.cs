using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModels
{
    public class PageBaseViewModel
    {
        #region Searching and Ordering    
        //Used for searching and ordering
        public int TotalRowCount { get; set; }
        public int Page { get; set; }
        public int RowsPerPage { get; set; }

        public int GetStartingRecord
        {
            get
            {
                return (Page - 1) * RowsPerPage;
            }
        }
        public int GetEndingRecord
        {
            get
            {
                return Page * RowsPerPage;
            }
        }

        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string Search { get; set; }
        #endregion

        public string AddSortClass(string column)
        {
            if (column.Equals(SortColumn, StringComparison.OrdinalIgnoreCase))
            {
                if (SortOrder.Equals("Asc", StringComparison.OrdinalIgnoreCase))
                {
                    return "sorting_asc";
                }
                else if (SortOrder.Equals("Desc", StringComparison.OrdinalIgnoreCase))
                {
                    return "sorting_desc";
                }
            }
            return "sorting";
        }

        public string AddSortEvent(string listJSName, string columnName)
        {
            return $"Inventory.{listJSName}.SetSortOrder('{columnName}')";
        }

        public string AddSortClass(string sortColumn, string sortOrder, string column)
        {
            if (column.Equals(sortColumn, StringComparison.OrdinalIgnoreCase))
            {
                if (sortOrder.Equals("Asc", StringComparison.OrdinalIgnoreCase))
                {
                    return "sorting_asc";
                }
                else if (sortOrder.Equals("Desc", StringComparison.OrdinalIgnoreCase))
                {
                    return "sorting_desc";
                }
            }
            return "sorting";
        }

        public List<int> PaginatePages()
        {
            return PaginatePages(TotalRowCount);
        }

        public List<int> PaginatePages(int totalRowCount)
        {
            List<int> _pages = new List<int>();

            //We figure out the total number of pages
            int _totalPages = (int)Math.Ceiling((decimal)totalRowCount / (decimal)RowsPerPage);
            int _startingPage = 1;

            if (Page > 5)
            {
                _startingPage = Page - 5;
            }

            for (int _page = _startingPage; _page < _startingPage + 10; _page++)
            {
                if (_page <= _totalPages)
                {
                    _pages.Add(_page);
                }
            }

            return _pages;
        }
    }
}
