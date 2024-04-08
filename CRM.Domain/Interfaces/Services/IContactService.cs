using CRM.Domain.Contracts.Contact;

namespace CRM.Domain.Interfaces.Services;

public interface IContactService
{
    Task<BaseResult<List<ContactResponse>>> GetAllContactsAsync(ContactStatus filterByStatus);
    Task<BaseResult<ContactResponse>> CreateContactAsync(ContactCreateRequest requestDto);
    Task<BaseResult<ContactResponse>> ChangeContactStatusAsync(long contactId, ContactStatus newStatus);
    Task<BaseResult<ContactResponse>> ContactPartialUpdateAsync(long contactId, ContactPartialUpdateRequest requestDto);
}
