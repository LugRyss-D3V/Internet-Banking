
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Interfaces.Services.Core;
using InternetBanking.Core.Application.Services;
using InternetBanking.Core.Application.Services.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InternetBanking.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));

            services.AddTransient<IBeneficiaryService, BeneficiaryService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
