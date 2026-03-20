using AutoMapper;
using Lead.Application.DTOs;
using Lead.Application.DTOs.Response;
using Lead.Domain.Entities;

namespace Lead.Application.Mappings;

/// <summary>
/// Specific mapping profile for Lead entity and its DTOs
/// </summary>
public class LeadMappingProfile : Profile
{
    public LeadMappingProfile()
    {
        // CreateLeadDto -> Leads entity
        CreateMap<CreateLeadDto, Leads>()
            .ForMember(dest => dest.LeadId, opt => opt.Ignore())
            .ForMember(dest => dest.DateOpened, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Domain.Enums.LeadStatus.New))
            .ForMember(dest => dest.EstimatedValue, opt => opt.MapFrom(src => (decimal)src.EstimatedValue));

        // Leads entity -> LeadResponse
        CreateMap<Leads, LeadResponse>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.FullAddress))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.DaysOpen, opt => opt.MapFrom(src => src.DaysOpen));

        // Reverse mapping for updates (if needed)
        CreateMap<LeadResponse, Leads>()
            .ForMember(dest => dest.FullName, opt => opt.Ignore())
            .ForMember(dest => dest.FullAddress, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.DaysOpen, opt => opt.Ignore());

        CreateMap<LeadNote, LeadNoteResponse>()
              .ForMember(dest => dest.NoteText, opt => opt.MapFrom(src => src.NoteText))
            .ForMember(dest => dest.LeadNoteId, opt => opt.MapFrom(src => src.LeadNoteId))
            .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => src.LeadId))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

        CreateMap<LeadActivity, LeadActivityResponse>();

        CreateMap<LeadDocument, LeadDocumentResponse>();


    }
}