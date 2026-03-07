using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;

namespace Project.Application.DTOs.Response
{
    public class ProjectSearchResponse<T>
    {
        public PagedList<ProjectResponse> projects { get; set; }
        public int totalRecords { get; set; }
        public List<Filter<T>> filters { get; set; }
    }
}
