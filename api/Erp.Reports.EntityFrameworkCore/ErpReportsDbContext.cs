using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Erp.Reports.ReportDefinitions;

namespace Erp.Reports.EntityFrameworkCore;

[ConnectionStringName("SqlServer")]
public class ErpReportsDbContext : AbpDbContext<ErpReportsDbContext>
{
    public DbSet<ReportDefinition> ReportDefinitions { get; set; }

    public ErpReportsDbContext(DbContextOptions<ErpReportsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureReports();
    }
}
