using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.Interfaces
{
    public interface ILeadNoteService
    {
        Task<LeadNoteResponse> CreateAsync(int leadId, LeadNoteRequest dto, CancellationToken cancellationToken);
        Task<IReadOnlyList<LeadNoteResponse>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken);
        Task<LeadNoteResponse?> UpdateAsync(int leadId, int noteId, LeadNoteRequest dto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int leadId, int noteId, CancellationToken cancellationToken);
    }
}
