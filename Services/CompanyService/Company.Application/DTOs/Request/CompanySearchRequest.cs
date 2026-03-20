using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.DTOs.Request
{
    public class CompanySearchRequest<T> : PageRequestParameters
    {
        public List<Filter<T>>? Filters { get; set; }
        
    }
}
