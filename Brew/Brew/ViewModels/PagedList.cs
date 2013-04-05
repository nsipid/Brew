using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels
{
    public class PagedList<T>
    {
        private readonly List<T> singlePage;

        public readonly int PageSize;

        public readonly uint LastPage;

        public readonly uint CurrentPageNumber;

        public bool HasNextPage
        {
            get { return CurrentPageNumber < LastPage; }
        }

        public bool HasPreviousPage
        {
            get { return CurrentPageNumber > 0; }
        }

        public List<T> CurrentPage
        {
            get { return singlePage; }
        }

        public PagedList(List<T> singlePage, uint lastPage, uint pageNumber = 0, int pageSize = 30)
        {
            this.singlePage = singlePage;
            LastPage = lastPage;
            CurrentPageNumber = pageNumber;
            PageSize = pageSize;

            if (pageNumber > lastPage)
                throw new ArgumentException("Page number cannot be greater than last page.");

            if (singlePage.Count < pageSize && pageNumber != lastPage)
                throw new ArgumentException("Only the last page can contain a page smaller than pageSize.");
        }
    }
}