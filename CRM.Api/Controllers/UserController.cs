using CRM.Domain.Contracts.User;
using CRM.Domain.Enums;
using CRM.Domain.Interfaces.Services;
using CRM.Domain.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("auth/register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<UserRegisterResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<UserRegisterResponse>>> RegisterUser(UserRegisterRequest request)
    {
        var result = await _userService.RegisterUserAsync(request);

        if (result.IsSuccess)
        {
            //todo: change to created  
            return Ok(result);
        }

        return BadRequest(new BaseResult { ErrorMessage = result.ErrorMessage });
    }

    [HttpPost("auth/login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    public async Task<ActionResult<BaseResult>> LoginUser(UserLoginRequest request)
    {
        var result = await _userService.LoginUserAsync(request);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return Unauthorized(result);
    }

    [Authorize]
    [HttpPost("auth/logout")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    public async Task<ActionResult<BaseResult>> LogoutUser()
    {
        var result = await _userService.LogoutUserAsync();

        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<List<UserResponse>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<List<UserResponse>>>> GetAllUsers()
    {
        var result = await _userService.GetAllUsersAsync();

        return Ok(result);
    }

    [Authorize]
    [HttpGet("profile")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<UserResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    public async Task<ActionResult<BaseResult<UserResponse>>> GetUserInfo()
    {
        var result = await _userService.GetCurrentUserAsync();

        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPatch("{id}/block")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult>> BlockUser(long id)
    {
        var result = await _userService.BlockUserAsync(id);

        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPatch("{id}/unblock")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult>> UnblockUser(long id)
    {
        var result = await _userService.UnblockUserAsync(id);

        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPatch("{id}/role")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<UserResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<UserResponse>>> ChangeUserRole(long id, UserRole newRole)
    {
        var result = await _userService.ChangeUserRoleAsync(id, newRole);

        if (!result.IsSuccess) return BadRequest(new BaseResult { ErrorMessage = result.ErrorMessage });

        return Ok(result);
    }

    [Authorize]
    [HttpPatch("change-password")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    public async Task<ActionResult<BaseResult>> ChangeUserRole(UserChangePasswordRequest requestDto)
    {
        var result = await _userService.ChangeCurrentUserPasswordAsync(requestDto);

        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult>> DeleteUser(long id)
    {
        var result = await _userService.DeleteUserByIdAsync(id);

        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }
}
