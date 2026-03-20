using Lead.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.Interfaces
{
    public interface ILeadActivityRepository:IRepositoryBase<LeadActivity>
    {
        Task<IReadOnlyList<LeadActivity>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken);
        Task<LeadActivity?> GetByIdAndLeadIdAsync(int leadId, int activityId, CancellationToken cancellationToken);
    }
}
