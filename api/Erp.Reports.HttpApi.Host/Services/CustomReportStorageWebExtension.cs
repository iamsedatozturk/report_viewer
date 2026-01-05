using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevExpress.XtraReports.UI;
using Erp.Reports.HttpApi.Host.PredefinedReports;
using Erp.Reports.EntityFrameworkCore;

namespace Erp.Reports.HttpApi.Host.Services;

public class CustomReportStorageWebExtension : DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension
{
    protected ErpReportsDbContext DbContext { get; set; }
    
    public CustomReportStorageWebExtension(ErpReportsDbContext dbContext)
    {
        this.DbContext = dbContext;
    }

    public override bool CanSetData(string url)
    {
        return true;
    }

    public override bool IsValidUrl(string url)
    {
        return true;
    }

    public override byte[] GetData(string url)
    {
        var reportData = DbContext.ReportDefinitions.FirstOrDefault(x => x.Name == url);
        if (reportData != null)
            return reportData.Content;

        if (ReportsFactory.Reports.ContainsKey(url))
        {
            using var ms = new MemoryStream();
            using XtraReport report = ReportsFactory.Reports[url]();
            report.SaveLayoutToXml(ms);
            return ms.ToArray();
        }
        throw new DevExpress.XtraReports.Web.ClientControls.FaultException($"Could not find report '{url}'.");
    }

    public override Dictionary<string, string> GetUrls()
    {
        return DbContext.ReportDefinitions
            .ToList()
            .Select(x => x.Name)
            .Union(ReportsFactory.Reports.Select(x => x.Key))
            .ToDictionary<string, string>(x => x);
    }

    public override void SetData(XtraReport report, string url)
    {
        using var stream = new MemoryStream();
        report.SaveLayoutToXml(stream);
        
        var reportData = DbContext.ReportDefinitions.FirstOrDefault(x => x.Name == url);
        if (reportData == null)
        {
            var newReport = new ReportDefinitions.ReportDefinition(
                Guid.NewGuid(),
                url,
                url,
                stream.ToArray()
            );
            DbContext.ReportDefinitions.Add(newReport);
        }
        else
        {
            reportData.UpdateContent(stream.ToArray());
        }
        DbContext.SaveChanges();
    }

    public override string SetNewData(XtraReport report, string defaultUrl)
    {
        SetData(report, defaultUrl);
        return defaultUrl;
    }
}
