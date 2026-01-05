using System.Collections.Generic;
using System.Linq;
using DevExpress.DataAccess.Json;
using DevExpress.DataAccess.Web;
using Erp.Reports.HttpApi.Host.Data;

namespace Erp.Reports.HttpApi.Host.Services;

public class CustomJsonDataConnectionProviderFactory : IJsonDataConnectionProviderFactory
{
    protected LegacyReportDbContext DbContext { get; }

    public CustomJsonDataConnectionProviderFactory(LegacyReportDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public IJsonDataConnectionProviderService Create()
    {
        return new WebDocumentViewerJsonDataConnectionProvider(DbContext.JsonDataConnections.ToList());
    }
}

public class WebDocumentViewerJsonDataConnectionProvider : IJsonDataConnectionProviderService
{
    readonly IEnumerable<DataConnection> jsonDataConnections;
    
    public WebDocumentViewerJsonDataConnectionProvider(IEnumerable<DataConnection> jsonDataConnections)
    {
        this.jsonDataConnections = jsonDataConnections;
    }
    
    public JsonDataConnection GetJsonDataConnection(string name)
    {
        var connection = jsonDataConnections.FirstOrDefault(x => x.Name == name);
        if (connection == null)
            throw new InvalidOperationException();
        return CustomDataSourceWizardJsonDataConnectionStorage.CreateJsonDataConnectionFromString(connection);
    }
}
