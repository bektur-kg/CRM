using CRM.Domain.Contracts.User;
using CRM.Domain.Interfaces.Services;
using CRM.Domain.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("auth/register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<UserRegisterResponse>>> RegisterUser(UserRegisterRequest request)
    {
        var result = await _userService.RegisterUserAsync(request);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPost("auth/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult>> LoginUser(UserLoginRequest request)
    {
        var result = await _userService.LoginUserAsync(request);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return Unauthorized(result);
    }

    [HttpGet("test")]
    [Authorize]
    public async Task<ActionResult<BaseResult>> GetTest()
    {
        return Ok("woieowie");
    }
}
