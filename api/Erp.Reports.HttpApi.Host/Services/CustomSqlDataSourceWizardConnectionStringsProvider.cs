using System.Collections.Generic;
using System.Linq;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;
using Erp.Reports.HttpApi.Host.Data;

namespace Erp.Reports.HttpApi.Host.Services;

public class CustomSqlDataSourceWizardConnectionStringsProvider : IDataSourceWizardConnectionStringsProvider
{
    readonly LegacyReportDbContext reportDataContext;

    public CustomSqlDataSourceWizardConnectionStringsProvider(LegacyReportDbContext reportDataContext)
    {
        this.reportDataContext = reportDataContext;
    }
    
    Dictionary<string, string> IDataSourceWizardConnectionStringsProvider.GetConnectionDescriptions()
    {
        return reportDataContext.SqlDataConnections.ToDictionary(x => x.Name, x => x.DisplayName);
    }

    DataConnectionParametersBase IDataSourceWizardConnectionStringsProvider.GetDataConnectionParameters(string name)
    {
        return null;
    }
}
