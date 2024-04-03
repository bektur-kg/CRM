using CRM.Domain.Contracts.User;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Domain.Results;

namespace CRM.Domain.Interfaces.Validations;

public interface IUserValidation
{
    BaseResult ValidateOnCreate(User user);
    BaseResult ValidateForNull(User user);
    BaseResult ValidateOnLogin(User user, UserLoginRequest userDto);
    BaseResult ValidateOnBlock(User userToBlock, long currentUserId);
    BaseResult ValidateOnUnblock(User userToUnblock);
    BaseResult ValidateOnDelete(User userToDelete, long currentUserId);
    BaseResult ValidateOnRoleChange(User user, UserRole newRole, long currentUserId);
    BaseResult ValidateOnPasswordChange(User currentUser, UserChangePasswordRequest requestDto);
}
