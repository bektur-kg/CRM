namespace CRM.Domain.Interfaces.Validations;

public interface IContactValidation
{
    BaseResult ValidateOnCreate(Contact contact);
    BaseResult ValidateForNull(Contact contact);
    BaseResult ValidateOnContactStatusChange(Contact contact, ContactStatus newStatus);
    BaseResult ValidateOnContactPartialUpdate(Contact contact, long? marketerId, bool doesMarketerExist);
}
