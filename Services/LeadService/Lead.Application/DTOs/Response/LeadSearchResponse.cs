using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.DTOs.Response
{
    public class LeadSearchResponse<T>
    {
        public PagedList<LeadResponse> leads { get; set; }
        public int totalRecords { get; set; }
        public List<Filter<T>> filters { get; set; }
    }
}
