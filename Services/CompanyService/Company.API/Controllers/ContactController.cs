using Company.Application.DTOs;
using Company.Application.DTOs.Request;
using Company.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Models.Helpers;

namespace Company.API.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactController : ControllerBase
{
    private readonly IContactService _service;

    public ContactController(IContactService service)
    {
        _service = service;
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create(
        [FromBody] CreateContactDTO dto, int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id == 0)
        {
            await _service.CreateAsync(dto, cancellationToken);
            return Ok(ResponseHelper.CreateSuccessResponse(
                null, message: "Contact created successfully"));
        }

        await _service.UpdateAsync(id, dto, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(
            null, message: "Contact updated successfully"));
    }

    [HttpPost("search")]
    public async Task<ResponseModel> Search(
        [FromBody] ContactSearchRequest<object> search,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _service.SearchAsync(search, cancellationToken);
            return ResponseHelper.CreateSuccessResponse(
                response, message: "Contacts retrieved successfully");
        }
        catch (Exception ex)
        {
            return ResponseHelper.CreateErrorResponse(
                System.Net.HttpStatusCode.BadRequest,
                new Exception($"Error: {ex.Message}"));
        }
    }

    /// <summary>
    /// Get all contacts for a company (used by Project Service lookup).
    /// </summary>
    [HttpGet("by-company/{companyId}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> GetByCompany(
        [FromRoute] int companyId, CancellationToken cancellationToken = default)
    {
        var contacts = await _service.GetContactsByCompanyId(companyId, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(
            contacts, message: "Contacts retrieved successfully"));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> GetContact(
        [FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var contact = await _service.GetContactById(id, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(
            contact, message: "Contact retrieved successfully"));
    }
}