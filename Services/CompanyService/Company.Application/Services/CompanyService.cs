using AutoMapper;
using Company.Application.DTOs;
using Company.Application.DTOs.Request;
using Company.Application.DTOs.Response;
using Company.Application.Interfaces;
using Company.Domain.Entities;
using Shared.Models.Utilities.Paging;

namespace Company.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateCompanyDTO dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Companies>(dto);

        // Map trades
        if (dto.Trades.Count > 0)
        {
            // Trades will be saved via the repository after company is created
        }

        await _companyRepository.CreateAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(int id, CreateCompanyDTO dto, CancellationToken cancellationToken)
    {
        var existing = _companyRepository.FindByCondition(c => c.CompanyId == id).FirstOrDefault()
            ?? throw new ArgumentException($"Company with ID {id} not found.");

        _mapper.Map(dto, existing);
        await _companyRepository.UpdateAsync(existing, cancellationToken);
    }

    public async Task<CompanyResponse?> GetCompanyById(int id, CancellationToken cancellationToken)
    {
        return await _companyRepository.GetCompanyById(id, cancellationToken);
    }

    public async Task<PagingResponseDto<CompanyResponse>> SearchAsync(
        CompanySearchRequest<object> request, CancellationToken cancellationToken)
    {
        return await _companyRepository.SearchAsync(request, cancellationToken);
    }
}