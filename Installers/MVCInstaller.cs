﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acclaim.Api.Installers
{
    public class MVCInstaller : IInstaller
    {

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Acclaim API", Version = "v1" });
            });
            services.ConfigureSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
            });

            //Enble CORS
            services.AddCors(Options =>
            {
                Options.AddPolicy("EnableCORS", builder =>
                 builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build());

            });
        }
    }
}
