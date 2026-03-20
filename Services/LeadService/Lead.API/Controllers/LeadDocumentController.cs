using Lead.Application.DTOs.Request;
using Lead.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Helpers;
using System.Reflection.Metadata;

namespace Lead.API.Controllers
{
    [Route("api/leads/{leadId:int}/documents")]
    [ApiController]
    public class LeadDocumentController : ControllerBase
    {
        private readonly ILeadDocumentService leadDocumentService;

        public LeadDocumentController(ILeadDocumentService leadDocumentService)
        {
            this.leadDocumentService = leadDocumentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel), 200)]

        public async Task<IActionResult> GetAll([FromRoute] int leadId, CancellationToken cancellationToken)
        {
            var documents = await leadDocumentService.GetByLeadIdAsync(leadId, cancellationToken);
            return Ok(ResponseHelper.CreateSuccessResponse(documents, "Lead documents retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Create(
          [FromRoute] int leadId,
          [FromBody] LeadDocumentRequest dto,
          CancellationToken cancellationToken)
        {
            var created = await leadDocumentService.CreateAsync(leadId, dto, cancellationToken);
            return Ok(ResponseHelper.CreateSuccessResponse(created, "Lead note created successfully"));
        }

        [HttpPut("{documentId:int}")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(
          [FromRoute] int leadId,
          [FromRoute] int documentId,
          [FromBody] LeadDocumentRequest dto,
          CancellationToken cancellationToken)
        {
            var updated = await leadDocumentService.UpdateAsync(leadId, documentId, dto, cancellationToken);
            if (updated is null)
            {
                return NotFound(ResponseHelper.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, new Exception("Note not found")));
            }

            return Ok(ResponseHelper.CreateSuccessResponse(updated, "Lead note updated successfully"));
        }

        [HttpDelete("{documentId:int}")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(
            [FromRoute] int leadId,
            [FromRoute] int documentId,
            CancellationToken cancellationToken)
        {
            var ok = await leadDocumentService.DeleteAsync(leadId, documentId, cancellationToken);
            if (!ok)
            {
                return NotFound(ResponseHelper.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, new Exception("Note not found")));
            }

            return Ok(ResponseHelper.CreateSuccessResponse(null, "Lead note deleted successfully"));
        }
    }
}
