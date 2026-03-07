using Lead.Application.DTOs;
using Lead.Application.DTOs.Request;
using Lead.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Models.Helpers;

namespace Lead.API.Controllers;


[ApiController]
[Route("api/leads")]
public class LeadController : ControllerBase
{
    private readonly ILeadService _service;

    public LeadController(ILeadService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create a new lead
    /// </summary>
    [HttpPost("{id}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreateLeadDto dto,int id,CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }

        if (id == 0)
        {
            await _service.CreateAsync(dto, cancellationToken);
            return Ok(ResponseHelper.CreateSuccessResponse(
            null,
            message: "Lead created successfully"
        ));
        }

        await _service.UpdateAsync(id, dto, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(
            null, 
            message: "Lead updated successfully"
        ));
    }

    /// <summary>
    /// Search leads with advanced filters
    /// </summary>
    [HttpPost("search")]
    public async Task<ResponseModel> Search(
        [FromBody] LeadSearchRequest<object> search,
        CancellationToken cancellationToken)
    {
        try
        {
            // Debug logging
            Console.WriteLine($"Search is null: {search == null}");
            Console.WriteLine($"Filters count: {search?.Filters?.Count ?? 0}");
            
            if (search?.Filters != null)
            {
                foreach (var filter in search.Filters)
                {
                    Console.WriteLine($"Filter: {filter.Id} = {filter.Value}, Op: {filter.Operation}");
                }
            }

            var response = await _service.SearchAsync(search, cancellationToken);
            return ResponseHelper.CreateSuccessResponse(
                response,
                message: "Leads retrieved successfully"
            );
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            return ResponseHelper.CreateErrorResponse(
                System.Net.HttpStatusCode.BadRequest,  
                new Exception($"Error: {ex.Message}")
            );
        }
    }

    /// <summary>
    /// Get all leads with simple pagination
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var searchRequest = new LeadSearchRequest<object>
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await _service.SearchAsync(searchRequest, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(
            response,
            message: "Leads retrieved successfully"
        ));
    }

    /// <summary>
    /// Get lead by id
    /// </summary>
    /// 
    /// []
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> GetLead(
        [FromRoute]int id,
        CancellationToken cancellationToken = default)
    {
        var lead = await _service.GetLeadById(id, cancellationToken);

        return Ok(ResponseHelper.CreateSuccessResponse(
            lead,
            message: "Lead retrieved successfully"
        ));
    }
}
