using System.ComponentModel.DataAnnotations;

namespace Erp.Reports.ReportDefinitions;

public class UpdateReportDefinitionDto
{
    [StringLength(512)]
    public string DisplayName { get; set; }

    public byte[] Content { get; set; }
}
