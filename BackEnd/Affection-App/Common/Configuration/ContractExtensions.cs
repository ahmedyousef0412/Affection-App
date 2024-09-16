
namespace Affection.Contract.Configuration;

public static  class ContractExtensions
{
    public static IServiceCollection AddContract(this IServiceCollection services)
    {

       
        
        services.AddFluentValidationAutoValidation()
           .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

       

        return services;

    }
}
