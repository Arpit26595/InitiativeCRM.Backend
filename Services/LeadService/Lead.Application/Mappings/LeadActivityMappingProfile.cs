using AutoMapper;
using Lead.Application.DTOs.Response;
using Lead.Domain.Entities;
using Lead.Domain.Enums;

namespace Lead.Application.Mappings;

public class LeadActivityMappingProfile : Profile
{
    public LeadActivityMappingProfile()
    {
        CreateMap<LeadActivity, LeadActivityResponse>()
            .ForMember(d => d.Status,
                o => o.MapFrom(s => Enum.Parse<LeadActivityStatus>(s.Status)))
            .ForMember(d => d.Priority,
                o => o.MapFrom(s => Enum.Parse<LeadActivityPriority>(s.Priority)))
            .ForMember(d => d.ActivityType,
                o => o.MapFrom(s => Enum.Parse<LeadActivityType>(s.ActivityType)));
    }
}