using AutoMapper;
using Erp.Reports.ReportDefinitions;

namespace Erp.Reports;

public class ErpReportsApplicationAutoMapperProfile : Profile
{
    public ErpReportsApplicationAutoMapperProfile()
    {
        CreateMap<ReportDefinition, ReportDefinitionDto>();
    }
}
