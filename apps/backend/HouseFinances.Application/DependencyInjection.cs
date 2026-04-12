using HouseFinances.Application.Categories;
using HouseFinances.Application.Persons;
using HouseFinances.Application.Totals;
using HouseFinances.Application.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace HouseFinances.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ITotalsService, TotalsService>();
        return services;
    }
}
