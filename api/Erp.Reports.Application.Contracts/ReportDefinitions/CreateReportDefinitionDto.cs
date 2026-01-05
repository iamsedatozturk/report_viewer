using System.ComponentModel.DataAnnotations;

namespace Erp.Reports.ReportDefinitions;

public class CreateReportDefinitionDto
{
    [Required]
    [StringLength(256)]
    public string Name { get; set; }

    [Required]
    [StringLength(512)]
    public string DisplayName { get; set; }

    [Required]
    public byte[] Content { get; set; }
}
