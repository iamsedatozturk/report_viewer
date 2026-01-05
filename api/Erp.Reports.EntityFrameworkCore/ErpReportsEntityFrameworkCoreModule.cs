using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Erp.Reports.EntityFrameworkCore;

[DependsOn(
    typeof(ErpReportsDomainModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule)
)]
public class ErpReportsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ErpReportsDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });
    }
}
