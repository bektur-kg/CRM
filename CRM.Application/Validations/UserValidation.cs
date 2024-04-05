using CRM.Application.Resources;
using CRM.Domain.Contracts.User;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Domain.Interfaces.Tools;
using CRM.Domain.Interfaces.Validations;
using CRM.Domain.Results;

namespace CRM.Application.Validations;

public class UserValidation : IUserValidation
{
    private readonly IPasswordHasher _passwordHasher;

    public UserValidation(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public BaseResult ValidateOnCreate(User user)
    {
        if (user != null) return new BaseResult { ErrorMessage = ResultMessages.UserAlreadyExists };

        return new BaseResult();
    }

    public BaseResult ValidateForNull(User user)
    {
        if (user == null) return new BaseResult { ErrorMessage = ResultMessages.UserNotFound };

        return new BaseResult();
    }

    public BaseResult ValidateOnLogin(User user, UserLoginRequest userDto)
    {
        var result = ValidateForNull(user);
        if (!result.IsSuccess) return result;

        if (user.BlockDate != null) return new BaseResult { ErrorMessage = ResultMessages.AccountIsBlocked };

        var isVerified = _passwordHasher.Verify(userDto.Password, user.PasswordHash);
        if (!isVerified) return new BaseResult{ ErrorMessage = ResultMessages.IncorrectPassword };

        return new BaseResult();
    }

    public BaseResult ValidateOnBlock(User userToBlock, long currentUserId)
    {
        var result = ValidateForNull(userToBlock);  

        if (!result.IsSuccess) return result;

        if (userToBlock.Id == currentUserId)
        {
            return new BaseResult 
            {
                ErrorMessage = ResultMessages.BlockCurrentUserError
            };
        }

        if (userToBlock.BlockDate != null) return new BaseResult { ErrorMessage = ResultMessages.UserIsAlreadyBlocked };

        return new BaseResult();
    }

    public BaseResult ValidateOnUnblock(User userToUnblock)
    {
        var result = ValidateForNull(userToUnblock);

        if (!result.IsSuccess) return result;

        if (userToUnblock.BlockDate == null) return new BaseResult { ErrorMessage = ResultMessages.UserIsAlreadyUnblocked };

        return new BaseResult();
    }

    public BaseResult ValidateOnDelete(User userToDelete, long currentUserId)
    {
        var result = ValidateForNull(userToDelete);
        if (!result.IsSuccess) return result;

        if (userToDelete.Id == currentUserId) return new BaseResult { ErrorMessage = ResultMessages.DeleteCurrentUserError };

        return new BaseResult();
    }

    public BaseResult ValidateOnRoleChange(User user, UserRole newRole, long currentUserId)
    {
        var result = ValidateForNull(user);
        if (!result.IsSuccess) return result;

        if(user.Id == currentUserId) return new BaseResult { ErrorMessage = ResultMessages.ChangeCurrentUserRoleError };

        if(user.Role == newRole) return new BaseResult { ErrorMessage = ResultMessages.SameRoleError };

        return new BaseResult();
    }

    public BaseResult ValidateOnPasswordChange(User currentUser, UserChangePasswordRequest requestDto)
    {
        var isOldPasswordCorrect = _passwordHasher.Verify(requestDto.OldPassword, currentUser.PasswordHash);
        if (!isOldPasswordCorrect) return new BaseResult { ErrorMessage = ResultMessages.IncorrectPassword };

        var arePasswordsSimilar = _passwordHasher.Verify(requestDto.NewPassword, currentUser.PasswordHash);
        if (arePasswordsSimilar) return new BaseResult { ErrorMessage = ResultMessages.SamePasswordError };

        return new BaseResult();
    }
}
