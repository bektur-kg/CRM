using AutoMapper;
using CRM.Domain.Contracts.User;
using CRM.Domain.Entities;

namespace CRM.Application.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<UserRegisterRequest, User>();
        CreateMap<User, UserRegisterResponse>();
    }
}
