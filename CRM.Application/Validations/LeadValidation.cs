namespace CRM.Application.Validations;

public class LeadValidation : ILeadValidation
{
    public BaseResult ValidateForLeadStatus(LeadStatus status)
    {
        if (!Enum.IsDefined(typeof(LeadStatus), status)) return new BaseResult { ErrorMessage = ResultMessages.NotValidLeadStatus };

        return new BaseResult();
    }

    public BaseResult ValidateOnCreateLead(bool doesLeadExist, bool doesContactExist, LeadStatus status)
    {
        if(doesLeadExist) return new BaseResult { ErrorMessage = ResultMessages.LeadAlreadyExists };

        if(!doesContactExist) return new BaseResult { ErrorMessage = ResultMessages.ContactNotFound };

        var result = ValidateForLeadStatus(status);

        if (!result.IsSuccess) return result;

        return new BaseResult();
    }

    public BaseResult ValidateOnLeadStatusChange(Lead lead, LeadStatus newStatus)
    {
        if(lead == null) return new BaseResult { ErrorMessage = ResultMessages.LeadNotFound };

        var result = ValidateForLeadStatus(newStatus);

        if (!result.IsSuccess) return result;

        return new BaseResult();
    }
}
