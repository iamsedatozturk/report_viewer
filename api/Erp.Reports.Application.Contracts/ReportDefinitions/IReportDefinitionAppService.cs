using Volo.Abp.Application.Services;

namespace Erp.Reports.ReportDefinitions;

public interface IReportDefinitionAppService : IApplicationService
{
    Task<ReportDefinitionDto> GetAsync(Guid id);
    Task<ReportDefinitionDto> GetByNameAsync(string name);
    Task<byte[]> GetContentAsync(string name);
    Task<ReportDefinitionDto> CreateAsync(CreateReportDefinitionDto input);
    Task<ReportDefinitionDto> UpdateAsync(Guid id, UpdateReportDefinitionDto input);
    Task DeleteAsync(Guid id);
}
