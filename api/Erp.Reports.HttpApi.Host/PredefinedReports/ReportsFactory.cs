using DevExpress.XtraReports.UI;

namespace Erp.Reports.HttpApi.Host.PredefinedReports;

public static class ReportsFactory
{
    public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>()
    {
        ["TestReport"] = () => new TestReport()
    };
}
