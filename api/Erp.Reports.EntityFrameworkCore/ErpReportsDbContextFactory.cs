using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Erp.Reports.EntityFrameworkCore;

public class ErpReportsDbContextFactory : IDesignTimeDbContextFactory<ErpReportsDbContext>
{
    public ErpReportsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ErpReportsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("SqlServer"));

        return new ErpReportsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Erp.Reports.HttpApi.Host/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
