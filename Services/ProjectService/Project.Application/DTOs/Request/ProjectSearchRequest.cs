using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Request
{
    public class ProjectSearchRequest<T> : PageRequestParameters
    {
        public List<Filter<T>>? Filters { get; set; }
        
    }
}
