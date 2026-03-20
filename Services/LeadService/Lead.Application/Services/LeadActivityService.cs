using AutoMapper;
using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using Lead.Application.Interfaces;
using Lead.Domain.Entities;
using Lead.Domain.Enums;

namespace Lead.Application.Services;

public class LeadActivityService : ILeadActivityService
{
    private readonly ILeadActivityRepository _repo;
    private readonly IMapper _mapper;

    public LeadActivityService(ILeadActivityRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<LeadActivityResponse> CreateAsync(
        int leadId,
        LeadActivityRequest dto,
        CancellationToken cancellationToken)
    {
        // ✅ Enums are stored as strings in DB (your entity uses string fields)
        // ✅ Backend JSON already supports enums as strings (JsonStringEnumConverter in Lead.API Program.cs)

        var entity = new LeadActivity
        {
            LeadId = leadId,

            ActivityType = dto.ActivityType.ToString(),
            Status = dto.Status.ToString(),
            Priority = dto.Priority.ToString(),

            Subject = dto.Subject,
            Description = dto.Description,

            ActivityDate = dto.ActivityDate,
            DueDate = dto.DueDate,

            IsDeleted = false,

            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,

            AssignedToUserId =  0,
            CreatedBy =  0,
            UpdatedBy =  0
        };

        await _repo.CreateAsync(entity, cancellationToken);
        return _mapper.Map<LeadActivityResponse>(entity);
    }

    public async Task<IReadOnlyList<LeadActivityResponse>> GetByLeadIdAsync(
        int leadId,
        CancellationToken cancellationToken)
    {
        var items = await _repo.GetByLeadIdAsync(leadId, cancellationToken);
        return items.Select(_mapper.Map<LeadActivityResponse>).ToList();
    }

    public async Task<LeadActivityResponse?> UpdateAsync(
        int leadId,
        int activityId,
        LeadActivityRequest dto,
        CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAndLeadIdAsync(leadId, activityId, cancellationToken);
        if (existing is null) return null;

        existing.ActivityType = dto.ActivityType.ToString();
        existing.Status = dto.Status.ToString();
        existing.Priority = dto.Priority.ToString();

        existing.Subject = dto.Subject;
        existing.Description = dto.Description;

        existing.ActivityDate = dto.ActivityDate;
        existing.DueDate = dto.DueDate;

        existing.IsDeleted = false;
        existing.UpdatedDate = DateTime.UtcNow;

        // Usually CreatedDate should NOT be overwritten on update.
        // If your DB requires it from dto, keep it. Otherwise remove the next line.
        // existing.CreatedDate = dto.CreatedDate;

        // Optional audit fields (only set if your request includes them)
      //  if (dto.AssignedToUserId.HasValue) existing.AssignedToUserId = dto.AssignedToUserId.Value;
       // if (dto.UpdatedBy.HasValue) existing.UpdatedBy = dto.UpdatedBy.Value;

        await _repo.UpdateAsync(existing, cancellationToken);
        return _mapper.Map<LeadActivityResponse>(existing);
    }

    public async Task<bool> DeleteAsync(
        int leadId,
        int activityId,
        CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAndLeadIdAsync(leadId, activityId, cancellationToken);
        if (existing is null) return false;

        await _repo.DeleteAsync(existing, cancellationToken);
        return true;
    }
}