namespace CRM.Domain.Interfaces.Validations;

public interface ILeadValidation
{
    BaseResult ValidateOnCreateLead(bool doesLeadExist, bool doesContactExist, LeadStatus status);
    BaseResult ValidateOnLeadStatusChange(Lead lead, LeadStatus newStatus);
}
