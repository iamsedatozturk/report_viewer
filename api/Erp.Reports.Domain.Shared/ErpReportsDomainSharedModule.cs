using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Erp.Reports;

public class ErpReportsDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ErpReportsDomainSharedModule>();
        });
    }
}
