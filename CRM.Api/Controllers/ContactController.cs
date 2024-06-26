﻿using CRM.Api.Constants;
using CRM.Domain.Contracts.Contact;

namespace CRM.Api.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [Authorize(Roles = UserRoleMatches.AdminOrMarketer)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<List<ContactResponse>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<List<ContactResponse>>>> GetAllContacts()
    {
        var result = await _contactService.GetAllContactsAsync(0);

        return Ok(result);
    }

    [Authorize(Roles = UserRoleMatches.Seller)]
    [HttpGet("leads")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<List<ContactResponse>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<List<ContactResponse>>>> GetAllLeadContacts()
    {
        var result = await _contactService.GetAllContactsAsync(ContactStatus.Lead);

        return Ok(result);
    }

    [Authorize(Roles = UserRoleMatches.Marketer)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<ContactResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<ContactResponse>>> CreateContact(ContactCreateRequest requestDto)
    {
        var result = await _contactService.CreateContactAsync(requestDto);

        if (!result.IsSuccess) return BadRequest(new BaseResult { ErrorMessage = result.ErrorMessage });

        return Ok(result);
    }

    [Authorize(Roles = UserRoleMatches.MarketerOrSeller)]
    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<ContactResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<ContactResponse>>> ChangeContactStatus(long id, ContactStatus newStatus)
    {
        var result = await _contactService.ChangeContactStatusAsync(id, newStatus);

        if (!result.IsSuccess) return BadRequest(new BaseResult { ErrorMessage = result.ErrorMessage });

        return Ok(result);
    }

    [Authorize(Roles = UserRoleMatches.MarketerOrSeller)]
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<ContactResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<ContactResponse>>> PartialUpdateContact(long id, ContactPartialUpdateRequest requestDto)
    {
        var result = await _contactService.ContactPartialUpdateAsync(id, requestDto);

        if (!result.IsSuccess) return BadRequest(new BaseResult { ErrorMessage = result.ErrorMessage });

        return Ok(result);
    }
}
