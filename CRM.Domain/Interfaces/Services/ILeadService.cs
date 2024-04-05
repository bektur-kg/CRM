using CRM.Domain.Contracts.Lead;
using CRM.Domain.Enums;
using CRM.Domain.Results;

namespace CRM.Domain.Interfaces.Services;

public interface ILeadService
{
    Task<BaseResult<List<LeadResponse>>> GetAllLeadsAsync(); 
    Task<BaseResult<LeadResponse>> CreateLeadAsync(LeadCreateRequest requestDto); 
    Task<BaseResult<LeadResponse>> ChangeLeadStatusAsync(long leadId, LeadStatus newStatus); 
}
