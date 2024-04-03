using AutoMapper;
using CRM.Application.Resources;
using CRM.DAL.DbContexts;
using CRM.Domain.Contracts.User;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Domain.Interfaces.Services;
using CRM.Domain.Interfaces.Tools;
using Microsoft.AspNetCore.Authentication;
using CRM.Domain.Interfaces.Validations;
using CRM.Domain.Results;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CRM.Application.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;
    private readonly IUserValidation _validation;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    private readonly HttpContext _httpContext;

    public UserService(AppDbContext dbContext, IPasswordHasher passwordHasher, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserValidation userValidation)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _httpContext = httpContextAccessor.HttpContext;
        _validation = userValidation;
    }

    public async Task<BaseResult> LoginUserAsync(UserLoginRequest userDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == userDto.Email);

        var result = _validation.ValidateForNull(user);

        if (!result.IsSuccess)
        {
            return new BaseResult
            {
                ErrorMessage = result.ErrorMessage
            };
        }

        var isVerified = _passwordHasher.Verify(userDto.Passord, user.PassordHash);

        if (!isVerified)
        {
            return new BaseResult
            {
                ErrorMessage = ResultMessages.IncorrectPassword
            };
        }

        await LoginWithHttpContextAsync(user.Email, user.Role, user.Id);

        return new BaseResult();
    }

    public async Task<BaseResult<UserRegisterResponse>> RegisterUserAsync(UserRegisterRequest userDto)
    {
        var existingUser = await _dbContext.Users.Where(user => user.Email == userDto.Email).FirstOrDefaultAsync();

        var result = _validation.ValidateOnCreate(existingUser);

        if (!result.IsSuccess)
        {
            return new BaseResult<UserRegisterResponse>
            {
                ErrorMessage = ResultMessages.UserAlreadyExists
            };
        }

        var hashedPassword = _passwordHasher.Generate(userDto.Password);

        var user = _mapper.Map<User>(userDto);
        user.PassordHash = hashedPassword;

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return new BaseResult<UserRegisterResponse>
        {
            Data = _mapper.Map<UserRegisterResponse>(user)
        };
    }

    private async Task LoginWithHttpContextAsync(string email, UserRole userRole, long id)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, userRole.ToString()),
            new Claim("Id", id.ToString()),
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await _httpContext.SignInAsync(claimsPrincipal);
    }

}
