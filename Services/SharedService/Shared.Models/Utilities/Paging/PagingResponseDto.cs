namespace Shared.Models.Utilities.Paging
{
    public class PagingResponseDto<TDto> where TDto : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public TDto[] Items { get; set; }
    }
}
