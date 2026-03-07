using System;
using System.Collections.Generic;

namespace Lead.Application.Common
{
    /// <summary>
    /// Generic paged response wrapper for list endpoints
    /// </summary>
    /// <typeparam name="T">Type of items in the data collection</typeparam>
    public class PagedResponse<T>
    {
        public List<T> Data { get; set; }
        public PaginationMetadata Pagination { get; set; }

        public PagedResponse()
        {
            Data = new List<T>();
            Pagination = new PaginationMetadata();
        }

        public PagedResponse(
            List<T> data,
            int currentPage,
            int pageSize,
            int totalItems)
        {
            Data = data ?? new List<T>();
            Pagination = new PaginationMetadata
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = CalculateTotalPages(totalItems, pageSize)
            };
        }

        public static PagedResponse<T> Empty(int pageSize = 10)
        {
            return new PagedResponse<T>(new List<T>(), 1, pageSize, 0);
        }

        private static int CalculateTotalPages(int totalItems, int pageSize)
        {
            if (pageSize <= 0) return 0;
            return (int)Math.Ceiling(totalItems / (double)pageSize);
        }
    }

    public class PaginationMetadata
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public int FirstItemIndex => (CurrentPage - 1) * PageSize;
        public int LastItemIndex => Math.Min(FirstItemIndex + PageSize - 1, TotalItems - 1);
    }
}