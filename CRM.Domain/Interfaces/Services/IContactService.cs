using CRM.Domain.Contracts.Contact;
using CRM.Domain.Enums;
using CRM.Domain.Results;

namespace CRM.Domain.Interfaces.Services;

public interface IContactService
{
    Task<BaseResult<List<ContactResponse>>> GetAllContactsAsync(ContactStatus filterByStatus);
    Task<BaseResult<ContactResponse>> CreateContactAsync(ContactCreateRequest requestDto);
    Task<BaseResult<ContactResponse>> ChangeContactStatusAsync(long contactId, ContactStatus newStatus);
    Task<BaseResult<ContactResponse>> ContactPartialUpdateAsync(long contactId, ContactPartialUpdateRequest requestDto);
}
