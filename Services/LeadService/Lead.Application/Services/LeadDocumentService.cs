using AutoMapper;
using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using Lead.Application.Interfaces;
using Lead.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Application.Services
{
    public class LeadDocumentService : ILeadDocumentService
    {

        private readonly ILeadDocumentRepository _repo;
        private readonly IMapper _mapper;

        public LeadDocumentService(ILeadDocumentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }   
        public async Task<bool> DeleteAsync(int leadId, int documentId, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAndLeadIdAsync(leadId, documentId, cancellationToken);
            if (existing is null) return false;

            await _repo.DeleteAsync(existing, cancellationToken);
            return true;
        }

        public async Task<LeadDocumentResponse> CreateAsync(int leadId, LeadDocumentRequest dto, CancellationToken cancellationToken)
        {
            var entity = new LeadDocument
            {
                LeadId = leadId,
                Description = dto.Description,
                FileName = dto.FileName,
                FilePath = dto.FilePath,
                FileSize = long.Parse(dto.FileSize),
                FileType = dto.FileType,
                IsDeleted=false,
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate= DateTime.UtcNow,
                UploadedByUserId = dto.UploadedByUserId,
                UploadedDate=dto.UploadedDate,
                CreatedDate = DateTime.UtcNow,
                
            };

            await _repo.CreateAsync(entity, cancellationToken);
            return _mapper.Map<LeadDocumentResponse>(entity);
        }

        public async Task<IReadOnlyList<LeadDocumentResponse>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken)
        {
            var items = await _repo.GetByLeadIdAsync(leadId, cancellationToken);
            return items.Select(_mapper.Map<LeadDocumentResponse>).ToList();
        }

        public async Task<LeadDocumentResponse?> UpdateAsync(int leadId, int documentId, LeadDocumentRequest dto, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAndLeadIdAsync(leadId, documentId, cancellationToken);
            if (existing is null) return null;

            existing.FilePath = dto.FilePath;
            existing.FileName=dto.FileName;
            existing.FileSize = long.Parse(dto.FileSize);   
            existing.FileType = dto.FileType;
            existing.Description = dto.Description;
            existing.UpdatedBy = dto.UpdatedBy; 
            existing.UploadedByUserId = dto.UploadedByUserId;
            existing.UploadedDate = DateTime.UtcNow;
            existing.UpdatedDate = DateTime.UtcNow;

            await _repo.UpdateAsync(existing, cancellationToken);
            return _mapper.Map<LeadDocumentResponse>(existing);
        }
    }
}
