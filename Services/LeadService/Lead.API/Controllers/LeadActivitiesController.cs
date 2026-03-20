using Lead.Application.DTOs.Request;
using Lead.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Models.Helpers;

namespace Lead.API.Controllers;

[ApiController]
[Route("api/leads/{leadId:int}/activities")]
public class LeadActivitiesController : ControllerBase
{
    private readonly ILeadActivityService _service;

    public LeadActivitiesController(ILeadActivityService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> Create(
        [FromRoute] int leadId,
        [FromBody] LeadActivityRequest dto,
        CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(leadId, dto, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(created, "Lead activity created successfully"));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> GetAll(
        [FromRoute] int leadId,
        CancellationToken cancellationToken)
    {
        var items = await _service.GetByLeadIdAsync(leadId, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(items, "Lead activities retrieved successfully"));
    }

    [HttpPut("{activityId:int}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(
        [FromRoute] int leadId,
        [FromRoute] int activityId,
        [FromBody] LeadActivityRequest dto,
        CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateAsync(leadId, activityId, dto, cancellationToken);
        if (updated is null)
        {
            return NotFound(ResponseHelper.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, new Exception("Activity not found")));
        }

        return Ok(ResponseHelper.CreateSuccessResponse(updated, "Lead activity updated successfully"));
    }

    [HttpDelete("{activityId:int}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(
        [FromRoute] int leadId,
        [FromRoute] int activityId,
        CancellationToken cancellationToken)
    {
        var ok = await _service.DeleteAsync(leadId, activityId, cancellationToken);
        if (!ok)
        {
            return NotFound(ResponseHelper.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, new Exception("Activity not found")));
        }

        return Ok(ResponseHelper.CreateSuccessResponse(null, "Lead activity deleted successfully"));
    }
}