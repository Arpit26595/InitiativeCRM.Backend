using Lead.Application.DTOs;
using Lead.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lead.API.Controllers;

[ApiController]
[Route("api/leads")]
public class LeadController : ControllerBase
{
    private readonly LeadService _service;

    public LeadController(LeadService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLeadDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
}
