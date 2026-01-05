using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Erp.Reports;

[DependsOn(
    typeof(ErpReportsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule)
)]
public class ErpReportsApplicationContractsModule : AbpModule
{
}
