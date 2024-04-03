using CRM.Domain.Contracts.User;
using CRM.Domain.Entities;
using CRM.Domain.Results;

namespace CRM.Domain.Interfaces.Services;

public interface IUserService
{
    Task<BaseResult> LoginUserAsync(UserLoginRequest userDto);
    Task<BaseResult<UserRegisterResponse>> RegisterUserAsync(UserRegisterRequest userDto);
}
