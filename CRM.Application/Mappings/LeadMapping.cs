using AutoMapper;
using CRM.Domain.Contracts.Lead;
using CRM.Domain.Entities;

namespace CRM.Application.Mappings;

public class LeadMapping : Profile
{
    public LeadMapping()
    {
        CreateMap<Lead, LeadResponse>();
        CreateMap<LeadCreateRequest, Lead>();
    }
}
