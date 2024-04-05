using AutoMapper;
using CRM.DAL.DbContexts;
using CRM.Domain.Contracts.Contact;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Domain.Interfaces.Services;
using CRM.Domain.Interfaces.Validations;
using CRM.Domain.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRM.Application.Services;

public class ContactService : IContactService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IContactValidation _validation;
    private readonly HttpContext _httpContext;

    public ContactService(AppDbContext dbContext, IMapper mapper, IContactValidation validation,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validation = validation;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public async Task<BaseResult<ContactResponse>> ContactPartialUpdateAsync(long contactId, ContactPartialUpdateRequest requestDto)
    {
        var contact = await _dbContext.Contacts.Include(contact => contact.Marketer).FirstOrDefaultAsync(contact => contact.Id == contactId);
        var doesMarketerExist = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Id == requestDto.MarketerId && user.Role == UserRole.Marketer);

        var result = _validation.ValidateOnContactPartialUpdate(contact, requestDto.MarketerId, doesMarketerExist);

        if(!result.IsSuccess) return new BaseResult<ContactResponse> { ErrorMessage = result.ErrorMessage };

        var marketerId = contact.MarketerId;

        _mapper.Map(requestDto, contact);

        if (requestDto.MarketerId == null) contact.MarketerId = marketerId;

        contact.LastUpdated = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        //find why marketer goes to null
        var updatedContact = await _dbContext.Contacts.Include(c => c.Marketer).FirstOrDefaultAsync(c => c.Id == c.Id);
        var response = _mapper.Map<ContactResponse>(updatedContact);

        return new BaseResult<ContactResponse> { Data = response };
    }

    public async Task<BaseResult<ContactResponse>> ChangeContactStatusAsync(long contactId, ContactStatus newStatus)
    {
        var contact = await _dbContext.Contacts.FirstOrDefaultAsync(contact => contact.Id == contactId);

        var result = _validation.ValidateOnContactStatusChange(contact, newStatus);

        if (!result.IsSuccess) return new BaseResult<ContactResponse> { ErrorMessage = result.ErrorMessage };

        contact.Status = newStatus;
        await _dbContext.SaveChangesAsync();

        return new BaseResult<ContactResponse>
        {
            Data = _mapper.Map<ContactResponse>(contact)
        };
    }

    public async Task<BaseResult<ContactResponse>> CreateContactAsync(ContactCreateRequest requestDto)
    {
        var existingContact = await _dbContext.Contacts
            .AsNoTracking()
            .Include(c => c.Marketer)
            .FirstOrDefaultAsync(contact => contact.PhoneNumber == requestDto.PhoneNumber);

        var result = _validation.ValidateOnCreate(existingContact);

        if(!result.IsSuccess) return new BaseResult<ContactResponse> { ErrorMessage = result.ErrorMessage };

        var newContact = _mapper.Map<Contact>(requestDto);
        newContact.MarketerId = long.Parse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        newContact.LastUpdated = DateTime.UtcNow;

        await _dbContext.Contacts.AddAsync(newContact);
        await _dbContext.SaveChangesAsync();

        return new BaseResult<ContactResponse> { Data = _mapper.Map<ContactResponse>(newContact) };
    }

    public async Task<BaseResult<List<ContactResponse>>> GetAllContactsAsync(ContactStatus filterByStatus)
    {
        var querry = _dbContext.Contacts
            .Include(contact => contact.Marketer)
            .AsNoTracking();

        if (filterByStatus != 0)
        {
            querry = querry.Where(contact => contact.Status == filterByStatus);
        }

        var contacts = await querry.ToListAsync();

        return new BaseResult<List<ContactResponse>>
        {
            Data = _mapper.Map<List<ContactResponse>>(contacts)
        };
    }
}
