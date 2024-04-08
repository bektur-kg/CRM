using CRM.Domain.Contracts.Contact;

namespace CRM.Application.Mappings;

public class ContactMapping : Profile
{
    public ContactMapping()
    {
        CreateMap<Contact, ContactResponse>();
        CreateMap<ContactCreateRequest, Contact>(); 
        CreateMap<ContactPartialUpdateRequest, Contact>()
            .ForAllMembers(opt => opt.Condition((_, _, property) => property != null));
    }
}
