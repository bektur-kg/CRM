namespace CRM.Application.Validations;

public class ContactValidation : IContactValidation
{
    public BaseResult ValidateForNull(Contact contact)
    {
        if (contact == null) return new BaseResult { ErrorMessage = ResultMessages.ContactNotFound };

        return new BaseResult();
    }

    public BaseResult ValidateOnContactPartialUpdate(Contact contact, long? marketerId, bool doesMarketerExist)
    {
        var result = ValidateForNull(contact);

        if(!result.IsSuccess) return result;

        if(marketerId != null && !doesMarketerExist)
        {
            return new BaseResult { ErrorMessage = ResultMessages.MarketerNotFound };
        }

        return new BaseResult();
    }

    public BaseResult ValidateOnContactStatusChange(Contact contact, ContactStatus newStatus)
    {
        var result = ValidateForNull(contact);

        if (!result.IsSuccess) return result;

        if (newStatus == 0) return new BaseResult { ErrorMessage = ResultMessages.InvalidContactStatus };

        if (contact.Status == newStatus) return new BaseResult { ErrorMessage = ResultMessages.SameContactStatusError };

        return new BaseResult();
    }

    public BaseResult ValidateOnCreate(Contact contact)
    {
        if (contact != null) { return new BaseResult { ErrorMessage = ResultMessages.ContactAlreadyExists }; }

        return new BaseResult();
    }
}
