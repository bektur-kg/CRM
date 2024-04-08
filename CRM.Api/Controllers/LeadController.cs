using CRM.Api.Constants;
using CRM.Domain.Contracts.Lead;

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

    [Authorize(Roles = UserRoleMatches.Seller)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<LeadResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<LeadResponse>>> GetAllLeads()
    {
        var result = await _leadService.GetAllLeadsAsync();

        return Ok(result);
    }

    [Authorize(Roles = UserRoleMatches.Seller)]
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

    [Authorize(Roles = UserRoleMatches.Seller)]
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
