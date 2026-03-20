using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.Interfaces
{
    public interface ILeadDocumentService
    {
        Task<LeadDocumentResponse> CreateAsync(int leadId, LeadDocumentRequest dto, CancellationToken cancellationToken);
        Task<IReadOnlyList<LeadDocumentResponse>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken);
        Task<LeadDocumentResponse?> UpdateAsync(int leadId, int documentId, LeadDocumentRequest dto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int leadId, int documentId, CancellationToken cancellationToken);
    }
}
