using System.Collections.Generic;
using System.Linq;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.Web;
using DevExpress.DataAccess.Wizard.Services;
using Erp.Reports.HttpApi.Host.Data;

namespace Erp.Reports.HttpApi.Host.Services;

public class CustomSqlDataConnectionProviderFactory : IConnectionProviderFactory
{
    private readonly LegacyReportDbContext reportDbContext;

    public CustomSqlDataConnectionProviderFactory(LegacyReportDbContext reportDbContext)
    {
        this.reportDbContext = reportDbContext;
    }

    public IConnectionProviderService Create()
    {
        return new CustomSqlConnectionProviderService(reportDbContext.SqlDataConnections.ToList());
    }
}

public class CustomSqlConnectionProviderService : IConnectionProviderService
{
    readonly IEnumerable<DataConnection> sqlDataConnections;

    public CustomSqlConnectionProviderService(IEnumerable<DataConnection> sqlDataConnections)
    {
        this.sqlDataConnections = sqlDataConnections;
    }
    
    public SqlDataConnection LoadConnection(string connectionName)
    {
        var sqlDataConnectionData = sqlDataConnections.FirstOrDefault(x => x.Name == connectionName);
        if (sqlDataConnectionData == null)
            throw new InvalidOperationException();

        if (string.IsNullOrEmpty(sqlDataConnectionData.ConnectionString))
            throw new KeyNotFoundException($"Connection string '{connectionName}' not found.");
        
        var connectionParameters = new CustomStringConnectionParameters(sqlDataConnectionData.ConnectionString);
        return new SqlDataConnection(connectionName, connectionParameters);
    }
}
