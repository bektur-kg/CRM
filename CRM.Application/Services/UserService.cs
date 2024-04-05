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

    public long CurrentUserId => long.Parse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public UserService(AppDbContext dbContext, IPasswordHasher passwordHasher, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IUserValidation userValidation)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _httpContext = httpContextAccessor.HttpContext;
        _validation = userValidation;
    }

    public async Task<BaseResult> LoginUserAsync(UserLoginRequest requestDto)
    {
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == requestDto.Email);

        var result = _validation.ValidateOnLogin(user, requestDto);

        if (!result.IsSuccess) return result;

        await LoginWithHttpContextAsync(user.Email, user.Role, user.Id);

        return new BaseResult();
    }

    public async Task<BaseResult<UserRegisterResponse>> RegisterUserAsync(UserRegisterRequest requestDto)
    {
        var existingUser = await _dbContext.Users.AsNoTracking().Where(user => user.Email == requestDto.Email).FirstOrDefaultAsync();

        var result = _validation.ValidateOnCreate(existingUser);

        if (!result.IsSuccess)
        {
            return new BaseResult<UserRegisterResponse>
            {
                ErrorMessage = ResultMessages.UserAlreadyExists
            };
        }

        var hashedPassword = _passwordHasher.Generate(requestDto.Password);

        var user = _mapper.Map<User>(requestDto);
        user.PasswordHash = hashedPassword;

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
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await _httpContext.SignInAsync(claimsPrincipal);
    }

    public async Task<BaseResult<List<UserResponse>>> GetAllUsersAsync()
    {
        var users = await _dbContext.Users.AsNoTracking().ToListAsync();

        return new BaseResult<List<UserResponse>>()
        {
            Data = _mapper.Map<List<UserResponse>>(users)
        };
    }

    public async Task<BaseResult<UserResponse>> GetCurrentUserAsync()
    {
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == CurrentUserId);

        return new BaseResult<UserResponse>
        {
            Data = _mapper.Map<UserResponse>(user)
        };
    }

    public async Task<BaseResult> BlockUserAsync(long userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

        var result = _validation.ValidateOnBlock(user, CurrentUserId);

        if (!result.IsSuccess) return result;

        user.BlockDate = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        return new BaseResult();
    }

    public async Task<BaseResult> UnblockUserAsync(long userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

        var result = _validation.ValidateOnUnblock(user);

        if (!result.IsSuccess) return result;

        user.BlockDate = null;
        await _dbContext.SaveChangesAsync();

        return new BaseResult();
    }

    public async Task<BaseResult> DeleteUserByIdAsync(long id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

        var result = _validation.ValidateOnDelete(user, CurrentUserId);

        if (!result.IsSuccess) return result;

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return new BaseResult();
    }

    public async Task<BaseResult<UserResponse>> ChangeUserRoleAsync(long userId, UserRole newRole)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

        var result = _validation.ValidateOnRoleChange(user, newRole, CurrentUserId);

        if (!result.IsSuccess) return new BaseResult<UserResponse> { ErrorMessage = result.ErrorMessage };

        user.Role = newRole;
        await _dbContext.SaveChangesAsync();

        return new BaseResult<UserResponse>
        {
            Data = _mapper.Map<UserResponse>(user),
        };
    }

    public async Task<BaseResult> ChangeCurrentUserPasswordAsync(UserChangePasswordRequest requestDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == CurrentUserId);

        var result = _validation.ValidateOnPasswordChange(user, requestDto);

        if (!result.IsSuccess) return result;

        var newHashedPassword = _passwordHasher.Generate(requestDto.NewPassword);
        user.PasswordHash = newHashedPassword;
        await _dbContext.SaveChangesAsync();

        return new BaseResult();
    }
}
