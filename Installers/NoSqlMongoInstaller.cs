using Acclaim.Api.Options;
using Acclaim.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;



namespace Acclaim.Api.Installers
{
    public class NoSqlMongoInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<AcclaimStoreDatabaseSettings>(
            configuration.GetSection(nameof(AcclaimStoreDatabaseSettings)));

            services.AddSingleton<IAcclaimStoreDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<AcclaimStoreDatabaseSettings>>().Value;
            });


            services.AddSingleton<IProvideAcclaimService, ProvideAcclaimService>();
        }
    }
}
