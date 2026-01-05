using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Erp.Reports.ReportDefinitions;

public class ReportDefinitionAppService : ApplicationService, IReportDefinitionAppService
{
    private readonly IRepository<ReportDefinition, Guid> _reportDefinitionRepository;

    public ReportDefinitionAppService(IRepository<ReportDefinition, Guid> reportDefinitionRepository)
    {
        _reportDefinitionRepository = reportDefinitionRepository;
    }

    public async Task<ReportDefinitionDto> GetAsync(Guid id)
    {
        var report = await _reportDefinitionRepository.GetAsync(id);
        return ObjectMapper.Map<ReportDefinition, ReportDefinitionDto>(report);
    }

    public async Task<ReportDefinitionDto> GetByNameAsync(string name)
    {
        var report = await _reportDefinitionRepository.FirstOrDefaultAsync(x => x.Name == name);
        if (report == null)
        {
            throw new Volo.Abp.UserFriendlyException($"Report '{name}' not found");
        }
        return ObjectMapper.Map<ReportDefinition, ReportDefinitionDto>(report);
    }

    public async Task<byte[]> GetContentAsync(string name)
    {
        var report = await _reportDefinitionRepository.FirstOrDefaultAsync(x => x.Name == name);
        if (report == null)
        {
            throw new Volo.Abp.UserFriendlyException($"Report '{name}' not found");
        }
        return report.Content;
    }

    public async Task<ReportDefinitionDto> CreateAsync(CreateReportDefinitionDto input)
    {
        var report = new ReportDefinition(
            GuidGenerator.Create(),
            input.Name,
            input.DisplayName,
            input.Content
        );

        await _reportDefinitionRepository.InsertAsync(report);
        return ObjectMapper.Map<ReportDefinition, ReportDefinitionDto>(report);
    }

    public async Task<ReportDefinitionDto> UpdateAsync(Guid id, UpdateReportDefinitionDto input)
    {
        var report = await _reportDefinitionRepository.GetAsync(id);

        if (!string.IsNullOrEmpty(input.DisplayName))
        {
            report.DisplayName = input.DisplayName;
        }

        if (input.Content != null && input.Content.Length > 0)
        {
            report.UpdateContent(input.Content);
        }

        await _reportDefinitionRepository.UpdateAsync(report);
        return ObjectMapper.Map<ReportDefinition, ReportDefinitionDto>(report);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _reportDefinitionRepository.DeleteAsync(id);
    }
}
