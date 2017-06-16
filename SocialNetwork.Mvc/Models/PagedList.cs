using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Mvc.Models
{
    public class PagedList<T> : IEnumerable<T>
    {
        public int PageSize { get; set; }
        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                if (value < 1 || value > PagesCount)
                    throw new ArgumentException();
                currentPage = value;
            }
        }
        public int PagesCount
        {
            get
            {
                return items.Count() / PageSize + ((items.Count() % PageSize) > 0 ? 1 : 0);
            }
        }
        private readonly IEnumerable<T> items;
        private int currentPage;
        public PagedList(IEnumerable<T> items, int pageSize, int currentPage = 1)
        {
            PageSize = pageSize;
            this.items = items;
            CurrentPage = currentPage;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var page = items.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
            foreach (var item in page)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}