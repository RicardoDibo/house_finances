using HouseFinances.Application.Auth;
using HouseFinances.Application.Categories;
using HouseFinances.Application.Totals;
using HouseFinances.Application.Transactions;
using HouseFinances.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace HouseFinances.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ITotalsService, TotalsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
