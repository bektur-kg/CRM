using CRM.Application.Resources;
using CRM.Domain.Entities;
using CRM.Domain.Interfaces.Validations;
using CRM.Domain.Results;

namespace CRM.Application.Validations;

public class UserValidation : IUserValidation
{
    public BaseResult ValidateOnCreate(User user)
    {
        if (user != null)
        {
            return new BaseResult
            {
                ErrorMessage = ResultMessages.UserAlreadyExists
            };
        }

        return new BaseResult();
    }

    public BaseResult ValidateForNull(User user)
    {
        if (user == null)
        {
            return new BaseResult
            {
                ErrorMessage = ResultMessages.UserNotFound
            };
        }

        return new BaseResult();
    }
}
