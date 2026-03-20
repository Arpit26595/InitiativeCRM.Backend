using AutoMapper;
using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using Lead.Application.Interfaces;
using Lead.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lead.Application.Services;

public class LeadNoteService : ILeadNoteService
{
    private readonly ILeadNoteRepository _repo;
    private readonly IMapper _mapper;

    public LeadNoteService(ILeadNoteRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<LeadNoteResponse> CreateAsync(int leadId, LeadNoteRequest dto, CancellationToken cancellationToken)
    {
        var entity = new LeadNote
        {
            LeadId = leadId,
            NoteText = dto.NoteText,
            CreatedDate = DateTime.UtcNow
        };

        await _repo.CreateAsync(entity, cancellationToken);
        return _mapper.Map<LeadNoteResponse>(entity);
    }

    public async Task<IReadOnlyList<LeadNoteResponse>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken)
    {
        var items = await _repo.GetByLeadIdAsync(leadId, cancellationToken);
        return items.Select(_mapper.Map<LeadNoteResponse>).ToList();
    }

    public async Task<LeadNoteResponse?> UpdateAsync(int leadId, int noteId, LeadNoteRequest dto, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAndLeadIdAsync(leadId, noteId, cancellationToken);
        if (existing is null) return null;

        existing.NoteText = dto.NoteText;
        existing.UpdatedDate = DateTime.UtcNow;

        await _repo.UpdateAsync(existing, cancellationToken);
        return _mapper.Map<LeadNoteResponse>(existing);
    }

    public async Task<bool> DeleteAsync(int leadId, int noteId, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAndLeadIdAsync(leadId, noteId, cancellationToken);
        if (existing is null) return false;

        await _repo.DeleteAsync(existing, cancellationToken);
        return true;
    }
}