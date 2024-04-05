using AutoMapper;
using CRM.Domain.Contracts.Contact;
using CRM.Domain.Entities;

namespace CRM.Application.Mappings;

public class ContactMapping : Profile
{
    public ContactMapping()
    {
        CreateMap<Contact, ContactResponse>();
        CreateMap<ContactCreateRequest, Contact>();
        CreateMap<ContactPartialUpdateRequest, Contact>()
            .ForAllMembers(opt => opt.Condition((source, _, property) => property != null || source.MarketerId != null));
    }
}
