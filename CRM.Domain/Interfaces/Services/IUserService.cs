using CRM.Domain.Contracts.User;
using CRM.Domain.Enums;
using CRM.Domain.Results;

namespace CRM.Domain.Interfaces.Services;

public interface IUserService
{
    Task<BaseResult> LoginUserAsync(UserLoginRequest requestDto);
    Task<BaseResult> LogoutUserAsync();
    Task<BaseResult<UserRegisterResponse>> RegisterUserAsync(UserRegisterRequest requestDto);
    Task<BaseResult<List<UserResponse>>> GetAllUsersAsync();
    Task<BaseResult<UserResponse>> GetCurrentUserAsync();
    Task<BaseResult> BlockUserAsync(long userId);
    Task<BaseResult> UnblockUserAsync(long userId);
    Task<BaseResult> DeleteUserByIdAsync(long id);
    Task<BaseResult<UserResponse>> ChangeUserRoleAsync(long userId, UserRole newRole);
    Task<BaseResult> ChangeCurrentUserPasswordAsync(UserChangePasswordRequest requestDto);
}
