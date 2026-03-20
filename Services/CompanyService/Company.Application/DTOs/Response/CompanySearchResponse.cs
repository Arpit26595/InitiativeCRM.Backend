using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;

namespace Company.Application.DTOs.Response
{
    public class CompanySearchResponse<T>
    {
        public PagedList<CompanyResponse> companies { get; set; }
        public int totalRecords { get; set; }
        public List<Filter<T>> filters { get; set; }
    }
}
