using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.Security.Resources;
using Erp.Reports.HttpApi.Host.Services;
using Erp.Reports.HttpApi.Host.Data;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Erp.Reports.EntityFrameworkCore;

namespace Erp.Reports.HttpApi.Host;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            
            AppDomain.CurrentDomain.SetData("DataDirectory", builder.Environment.ContentRootPath);
            
            builder.Host.UseAutofac();
            builder.Services.AddApplicationAsync<ErpReportsHttpApiHostModule>();

            // Legacy DevExpress DbContext for data connections (SQLite for DevExpress samples only)
            builder.Services.AddDbContext<LegacyReportDbContext>(options => 
                options.UseSqlite(builder.Configuration.GetConnectionString("ReportsDataConnectionString")));

            // DevExpress Reporting Services
            builder.Services.AddDevExpressControls();
            builder.Services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();
            builder.Services.AddMvc();
            
            builder.Services.ConfigureReportingServices(configurator =>
            {
                if (builder.Environment.IsDevelopment())
                    configurator.UseDevelopmentMode();

                configurator.ConfigureReportDesigner(designerConfigurator =>
                {
                    designerConfigurator.RegisterDataSourceWizardConnectionStringsProvider<CustomSqlDataSourceWizardConnectionStringsProvider>();
                    designerConfigurator.RegisterDataSourceWizardJsonConnectionStorage<CustomDataSourceWizardJsonDataConnectionStorage>(true);
                });
                
                configurator.ConfigureWebDocumentViewer(viewerConfigurator =>
                {
                    viewerConfigurator.UseCachedReportSourceBuilder();
                    viewerConfigurator.RegisterJsonDataConnectionProviderFactory<CustomJsonDataConnectionProviderFactory>();
                    viewerConfigurator.RegisterConnectionProviderFactory<CustomSqlDataConnectionProviderFactory>();
                });
            });

            var app = builder.Build();
            
            // Initialize legacy database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                services.GetService<LegacyReportDbContext>()?.InitializeDatabase();
            }

            await app.InitializeApplicationAsync();

            // DevExpress Security Settings
            var contentDirectoryAllowRule = DirectoryAccessRule.Allow(
                new DirectoryInfo(Path.Combine(app.Environment.ContentRootPath, "Content")).FullName);
            AccessSettings.ReportingSpecificResources.TrySetRules(contentDirectoryAllowRule, UrlAccessRule.Allow());

            app.UseDevExpressControls();
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex}");
            return 1;
        }
    }
}
