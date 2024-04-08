using CRM.Domain.Contracts.User;

namespace CRM.Application.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<UserRegisterRequest, User>();
        CreateMap<User, UserRegisterResponse>();
        CreateMap<User, UserResponse>();
        CreateMap<User, ContactUserResponse>();
        CreateMap<User, LeadUserResponse>();
    }
}
