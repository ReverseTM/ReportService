using System.Data.Common;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ReportService.Data.Entities;
using ReportService.Data.Interfaces;

namespace ReportService.Api.Services;

public class ReportServiceImpl : ReportSubmitter.ReportSubmitterBase
{
    private readonly ILogger<ReportServiceImpl> _logger;
    
    private readonly IReportRepository<ReportEntity> _reportRepository;
    private readonly IAuthorRepository<AuthorEntity> _authorRepository;
    
    public ReportServiceImpl(
        ILogger<ReportServiceImpl> logger,
        IReportRepository<ReportEntity> reportRepository,
        IAuthorRepository<AuthorEntity> authorRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
    }

    public override async Task<ReportResponse> SubmitReport(ReportRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Attempting to submit report.");

        try
        {
            var author = await _authorRepository.GetByEmail(request.Metadata.Author.Email);

            if (author == null)
            {
                _logger.LogInformation("Author not found.");
                return CreateReportResponse(false, "Author not found.");
            }

            var report = new ReportEntity
            {
                WordFileUrl = request.WordFileUrl,
                AuthorId = author.Id,
                ReferencedFiles = string.Join(";", request.Metadata.ReferencedFiles),
                RequestTime = request.Metadata.RequestTime.ToDateTime()
            };

            await _reportRepository.Add(report);

            _logger.LogInformation("Report submitted");

            return CreateReportResponse(true, "Report submitted successfully.");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "DbUpdateException: {ExceptionMessage}", ex.Message);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError(ex, "OperationCanceledException: {ExceptionMessage}", ex.Message);
        }
        catch (NpgsqlException ex)
        {
            _logger.LogError(ex, "NpgsqlException: {ExceptionMessage}", ex.Message);
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, "OperationCanceledException: {ExceptionMessage}", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception: {ExceptionMessage}", ex.Message);
        }
        
        return CreateReportResponse(false, "Failed to submit report.");
    }

    private static ReportResponse CreateReportResponse(bool success, string message)
    {
        return new ReportResponse
        {
            Success = success,
            Message = message
        };
    }
}