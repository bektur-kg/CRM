using CRM.Domain.Entities;
using CRM.Domain.Results;

namespace CRM.Domain.Interfaces.Validations;

public interface IUserValidation
{
    BaseResult ValidateOnCreate(User user);
    BaseResult ValidateForNull(User user);
}
