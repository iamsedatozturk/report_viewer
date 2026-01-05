using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Erp.Reports;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(ErpReportsDomainSharedModule)
)]
public class ErpReportsDomainModule : AbpModule
{
}
