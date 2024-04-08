namespace CRM.Application.Validations;

public class LeadValidation : ILeadValidation
{
    public BaseResult ValidateOnCreateLead(bool doesLeadExist, bool doesContactExist, LeadStatus status)
    {
        if(doesLeadExist) return new BaseResult { ErrorMessage = ResultMessages.LeadAlreadyExists };

        if (!doesContactExist) return new BaseResult { ErrorMessage = ResultMessages.ContactNotFound };

        return new BaseResult();
    }

    public BaseResult ValidateOnLeadStatusChange(Lead lead, LeadStatus newStatus)
    {
        if(lead == null) return new BaseResult { ErrorMessage = ResultMessages.LeadNotFound };

        return new BaseResult();
    }
}
