using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Erp.Reports.ReportDefinitions;

namespace Erp.Reports.EntityFrameworkCore;

public static class ErpReportsDbContextModelCreatingExtensions
{
    public static void ConfigureReports(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<ReportDefinition>(b =>
        {
            b.ToTable(ReportsConsts.DbTablePrefix + "ReportDefinitions", ReportsConsts.DbSchema);
            b.ConfigureByConvention();
            
            b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            b.Property(x => x.DisplayName).IsRequired().HasMaxLength(512);
            b.Property(x => x.Content).IsRequired();
            
            b.HasIndex(x => x.Name);
        });
    }
}
