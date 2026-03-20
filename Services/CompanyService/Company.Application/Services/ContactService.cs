using AutoMapper;
using Company.Application.DTOs;
using Company.Application.DTOs.Request;
using Company.Application.DTOs.Response;
using Company.Application.Interfaces;
using Company.Domain.Entities;
using Shared.Models.Utilities.Paging;

namespace Company.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public ContactService(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateContactDTO dto, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<Contacts>(dto);
            await _contactRepository.CreateAsync(entity, cancellationToken);
        }
        catch(Exception ex)
        {
            throw ex;
        }
        }

    public async Task UpdateAsync(int id, CreateContactDTO dto, CancellationToken cancellationToken)
    {
        var existing = _contactRepository.FindByCondition(c => c.ContactId == id).FirstOrDefault()
            ?? throw new ArgumentException($"Contact with ID {id} not found.");

        _mapper.Map(dto, existing);
        await _contactRepository.UpdateAsync(existing, cancellationToken);
    }

    public async Task<ContactResponse?> GetContactById(int id, CancellationToken cancellationToken)
    {
        return await _contactRepository.GetContactById(id, cancellationToken);
    }

    public async Task<List<ContactResponse>> GetContactsByCompanyId(
        int companyId, CancellationToken cancellationToken)
    {
        return await _contactRepository.GetContactsByCompanyId(companyId, cancellationToken);
    }

    public async Task<PagingResponseDto<ContactResponse>> SearchAsync(
        ContactSearchRequest<object> request, CancellationToken cancellationToken)
    {
        return await _contactRepository.SearchAsync(request, cancellationToken);
    }
}