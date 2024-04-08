using CRM.Application.Services;
using CRM.Application.Tools;
using CRM.Application.Validations;
using CRM.Domain.Interfaces.Services;
using CRM.Domain.Interfaces.Tools;
using CRM.Domain.Interfaces.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Application.DependencyInjections;

public static class ApplicationLayer
{
    public static void AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserService, UserService>();
        services.AddTransient<IUserValidation, UserValidation>();

        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IContactValidation, ContactValidation>();

        services.AddScoped<ILeadService, LeadService>();
        services.AddTransient<ILeadValidation, LeadValidation>();

        services.AddScoped<ISaleService, SaleService>();
        services.AddTransient<ISaleValidation, SaleValidation>();

        services.AddHttpContextAccessor();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddAuthentication().AddCookie("Cookies", authenticationOptions =>
        {
            authenticationOptions.ExpireTimeSpan = TimeSpan.FromMinutes(10);

            authenticationOptions.Events.OnRedirectToLogin = (redirectContext) =>
            {
                redirectContext.Response.StatusCode = 401;
                return Task.CompletedTask;
            };

            authenticationOptions.Events.OnRedirectToAccessDenied = (redirectContext) =>
            {
                redirectContext.Response.StatusCode = 403;
                return Task.CompletedTask;
            };
        });


    }
}
