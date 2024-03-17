using System.Collections.Generic;
using Domain.Interfaces;

namespace UserStorageService.Application.Models
{
    public class PagedListResult<T>
    {
        public IList<T> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class PagingInfo : IPagination
    {
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}