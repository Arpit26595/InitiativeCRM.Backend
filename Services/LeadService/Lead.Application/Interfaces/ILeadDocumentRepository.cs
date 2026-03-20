using Lead.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.Interfaces
{
    public interface ILeadDocumentRepository:IRepositoryBase<LeadDocument>
    {
        Task<IReadOnlyList<LeadDocument>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken);
        Task<LeadDocument?> GetByIdAndLeadIdAsync(int leadId, int documentId, CancellationToken cancellationToken);

    }
}
