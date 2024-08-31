using Grpc.Core;

namespace ReportService.Api.Services;

public class ReportServiceImpl : ReportSubmitter.ReportSubmitterBase
{
    private readonly ILogger<ReportServiceImpl> _logger;

    public ReportServiceImpl(ILogger<ReportServiceImpl> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override Task<ReportResponse> SubmitReport(ReportRequest request, ServerCallContext context)
    {
        return Task.FromResult(new ReportResponse {Success = true, Message = ""});
    }
}