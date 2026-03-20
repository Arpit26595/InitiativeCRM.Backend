using Lead.Domain.Entities;

namespace Lead.Application.Interfaces;

public interface ILeadNoteRepository : IRepositoryBase<LeadNote>
{
    Task<IReadOnlyList<LeadNote>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken);
    Task<LeadNote?> GetByIdAndLeadIdAsync(int leadId, int noteId, CancellationToken cancellationToken);
}