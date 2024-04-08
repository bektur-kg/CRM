using CRM.Domain.Contracts.Lead;

namespace CRM.Application.Mappings;

public class LeadMapping : Profile
{
    public LeadMapping()
    {
        CreateMap<Lead, LeadResponse>();
        CreateMap<Lead, LeadSaleResponse>();
        CreateMap<LeadCreateRequest, Lead>();
    }
}
