using System;
using Volo.Abp.Domain.Entities;

namespace Erp.Reports.ReportDefinitions;

public class ReportDefinition : Entity<Guid>
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public byte[] Content { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    protected ReportDefinition()
    {
    }

    public ReportDefinition(Guid id, string name, string displayName, byte[] content)
        : base(id)
    {
        Name = name;
        DisplayName = displayName;
        Content = content;
        CreationTime = DateTime.UtcNow;
    }

    public void UpdateContent(byte[] content)
    {
        Content = content;
        LastModificationTime = DateTime.UtcNow;
    }
}
