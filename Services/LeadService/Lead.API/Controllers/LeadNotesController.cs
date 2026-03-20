using Lead.Application.DTOs.Request;
using Lead.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Models.Helpers;

namespace Lead.API.Controllers;

[ApiController]
[Route("api/leads/{leadId:int}/notes")]
public class LeadNotesController : ControllerBase
{
    private readonly ILeadNoteService _service;

    public LeadNotesController(ILeadNoteService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> Create(
        [FromRoute] int leadId,
        [FromBody] LeadNoteRequest dto,
        CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(leadId, dto, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(created, "Lead note created successfully"));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> GetAll(
        [FromRoute] int leadId,
        CancellationToken cancellationToken)
    {
        var items = await _service.GetByLeadIdAsync(leadId, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(items, "Lead notes retrieved successfully"));
    }

    [HttpPut("{noteId:int}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(
        [FromRoute] int leadId,
        [FromRoute] int noteId,
        [FromBody] LeadNoteRequest dto,
        CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateAsync(leadId, noteId, dto, cancellationToken);
        if (updated is null)
        {
            return NotFound(ResponseHelper.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, new Exception("Note not found")));
        }

        return Ok(ResponseHelper.CreateSuccessResponse(updated, "Lead note updated successfully"));
    }

    [HttpDelete("{noteId:int}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(
        [FromRoute] int leadId,
        [FromRoute] int noteId,
        CancellationToken cancellationToken)
    {
        var ok = await _service.DeleteAsync(leadId, noteId, cancellationToken);
        if (!ok)
        {
            return NotFound(ResponseHelper.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, new Exception("Note not found")));
        }

        return Ok(ResponseHelper.CreateSuccessResponse(null, "Lead note deleted successfully"));
    }
}