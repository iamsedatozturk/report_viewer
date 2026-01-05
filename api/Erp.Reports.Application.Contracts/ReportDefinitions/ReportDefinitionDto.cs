using System;

namespace Erp.Reports.ReportDefinitions;

public class ReportDefinitionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
