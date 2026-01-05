using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Erp.Reports.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.Swashbuckle;

namespace Erp.Reports.HttpApi.Host;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpSwashbuckleModule),
    typeof(ErpReportsApplicationModule),
    typeof(ErpReportsEntityFrameworkCoreModule)
)]
public class ErpReportsHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        ConfigureConventionalControllers();
        ConfigureCors(context, configuration);
        ConfigureSwaggerServices(context);
    }

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(ErpReportsApplicationModule).Assembly);
        });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddPolicy("AllowCorsPolicy", builder =>
            {
                builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
        });
    }

    private void ConfigureSwaggerServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Erp.Reports API", Version = "v1" });
            options.DocInclusionPredicate((docName, description) => true);
            options.CustomSchemaIds(type => type.FullName);
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("AllowCorsPolicy");

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Erp.Reports API");
        });

        app.UseConfiguredEndpoints();
    }
}
