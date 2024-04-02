using CRM.Domain.Contracts;

namespace CRM.Domain.Interfaces;

public interface IUserService
{
    Task RegisterUser(UserRegisterDto userDto);
}
