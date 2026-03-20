using Company.Application.DTOs;
using Company.Application.DTOs.Request;
using Company.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Models.Helpers;

namespace Company.API.Controllers;

[ApiController]
[Route("api/companies")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _service;

    public CompanyController(ICompanyService service)
    {
        _service = service;
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create(
        [FromBody] CreateCompanyDTO dto, int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id == 0)
        {
            await _service.CreateAsync(dto, cancellationToken);
            return Ok(ResponseHelper.CreateSuccessResponse(
                null, message: "Company created successfully"));
        }

        await _service.UpdateAsync(id, dto, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(
            null, message: "Company updated successfully"));
    }

    [HttpPost("search")]
    public async Task<ResponseModel> Search(
        [FromBody] CompanySearchRequest<object> search,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _service.SearchAsync(search, cancellationToken);
            return ResponseHelper.CreateSuccessResponse(
                response, message: "Companies retrieved successfully");
        }
        catch (Exception ex)
        {
            return ResponseHelper.CreateErrorResponse(
                System.Net.HttpStatusCode.BadRequest,
                new Exception($"Error: {ex.Message}"));
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var searchRequest = new CompanySearchRequest<object>
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await _service.SearchAsync(searchRequest, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(
            response, message: "Companies retrieved successfully"));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public async Task<IActionResult> GetCompany(
        [FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var company = await _service.GetCompanyById(id, cancellationToken);
        return Ok(ResponseHelper.CreateSuccessResponse(
            company, message: "Company retrieved successfully"));
    }

    /// <summary>
    /// Get dynamic filter definitions for companies
    /// </summary>
    [HttpGet("filters")]
    [ProducesResponseType(typeof(ResponseModel), 200)]
    public IActionResult GetFilterDefinitions()
    {
        try
        {
            var filtersPath = Path.Combine(AppContext.BaseDirectory, "Filters", "company-filters.json");
            var json = System.IO.File.ReadAllText(filtersPath);
            var filters = System.Text.Json.JsonSerializer.Deserialize<object>(json);

            return Ok(ResponseHelper.CreateSuccessResponse(
                filters,
                message: "Company filter definitions retrieved"
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHelper.CreateErrorResponse(
                System.Net.HttpStatusCode.BadRequest,
                new Exception($"Error loading filters: {ex.Message}")
            ));
        }
    }
}