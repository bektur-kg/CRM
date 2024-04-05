using CRM.Domain.Contracts.Contact;
using CRM.Domain.Contracts.Lead;
using CRM.Domain.Enums;
using CRM.Domain.Interfaces.Services;
using CRM.Domain.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers;

[ApiController]
[Route("api/leads")]
public class LeadController : ControllerBase
{
    private readonly ILeadService _leadService;

    public LeadController(ILeadService leadService)
    {
        _leadService = leadService;
    }

    [Authorize(Roles = nameof(UserRole.Seller))]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<LeadResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<LeadResponse>>> GetAllLeads()
    {
        var result = await _leadService.GetAllLeadsAsync();

        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.Seller))]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<LeadResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<LeadResponse>>> CreateLead(LeadCreateRequest requestDto)
    {
        var result = await _leadService.CreateLeadAsync(requestDto);

        if (!result.IsSuccess) return BadRequest(new BaseResult { ErrorMessage = result.ErrorMessage});

        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.Seller))]
    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<LeadResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<LeadResponse>>> ChangeLeadStatus(long id, LeadStatus newStatus)
    {
        var result = await _leadService.ChangeLeadStatusAsync(id, newStatus);

        if (!result.IsSuccess) return BadRequest(new BaseResult { ErrorMessage = result.ErrorMessage });

        return Ok(result);
    }
}
