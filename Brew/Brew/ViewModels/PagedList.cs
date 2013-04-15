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

        public readonly int LastPage;

        public readonly int CurrentPageNumber;

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

        public PagedList(IOrderedQueryable<T> queryable, int pageNumber = 0, int pageSize = 30)
        {
            var queryablePage = queryable.Skip(pageSize * pageNumber).Take(pageSize);

            this.singlePage = queryablePage.ToList();
            CurrentPageNumber = pageNumber;
            PageSize = pageSize;
            LastPage = queryable.Count() / pageSize;
        }
    }
}