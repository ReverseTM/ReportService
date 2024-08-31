using Grpc.Core;
using ReportService.Data.Entities;
using ReportService.Data.Interfaces;

namespace ReportService.Api.Services;

public class ReportServiceImpl : ReportSubmitter.ReportSubmitterBase
{
    private readonly ILogger<ReportServiceImpl> _logger;
    private readonly IRepository<Report> _reportRepository;
    public ReportServiceImpl(ILogger<ReportServiceImpl> logger, IRepository<Report> reportRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
    }

    public override Task<ReportResponse> SubmitReport(ReportRequest request, ServerCallContext context)
    {
        return Task.FromResult(new ReportResponse {Success = true, Message = ""});
    }
}