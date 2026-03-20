using AutoMapper;
using Company.Application.DTOs;
using Company.Application.DTOs.Response;
using Company.Domain.Entities;

namespace Company.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateCompanyDTO → Companies entity
        CreateMap<CreateCompanyDTO, Companies>();

        // TradeDTO → CompanyTrades
        CreateMap<TradeDTO, CompanyTrades>();

        // CreateContactDTO → Contacts entity
        CreateMap<CreateContactDTO, Contacts>();

        // Contacts entity → ContactResponse
        CreateMap<Contacts, ContactResponse>()
            .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName));

        // CompanyTrades entity → CompanyTradeResponse
        CreateMap<CompanyTrades, CompanyTradeResponse>()
            .ForMember(d => d.TradeName, opt => opt.MapFrom(s => s.TradeName));
    }
}