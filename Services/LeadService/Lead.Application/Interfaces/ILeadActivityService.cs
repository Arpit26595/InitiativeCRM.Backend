using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.Interfaces
{
    public interface ILeadActivityService
    {
        Task<LeadActivityResponse> CreateAsync(int leadId, LeadActivityRequest dto, CancellationToken cancellationToken);
        Task<IReadOnlyList<LeadActivityResponse>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken);
        Task<LeadActivityResponse?> UpdateAsync(int leadId, int activityId, LeadActivityRequest dto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int leadId, int activityId, CancellationToken cancellationToken);


    }
}
