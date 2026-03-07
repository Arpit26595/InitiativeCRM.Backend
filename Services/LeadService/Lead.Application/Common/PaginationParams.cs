using System.ComponentModel.DataAnnotations;

namespace Lead.Application.Common
{
    /// <summary>
    /// Base class for pagination query parameters
    /// </summary>
    public class PaginationParams
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;

        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, MaxPageSize)]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        [StringLength(100)]
        public string? Search { get; set; }

        [StringLength(50)]
        public string? SortBy { get; set; }

        [RegularExpression("^(asc|desc)$", ErrorMessage = "SortDirection must be 'asc' or 'desc'")]
        public string SortDirection { get; set; } = "desc";
    }
}