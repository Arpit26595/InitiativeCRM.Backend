using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.DTOs.Request
{
    public class ContactSearchRequest<T> : PageRequestParameters
    {
        public int CompanyId { get; set; }
        public List<Filter<T>>? Filters { get; set; }

    }
}
