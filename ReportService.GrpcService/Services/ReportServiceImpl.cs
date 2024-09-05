using FluentValidation;
using Grpc.Core;

using ReportService.Data.Entities;
using ReportService.Data.Interfaces;
using ReportService.Grpc;

namespace ReportService.GrpcService.Services;

/// <summary>
/// Implementation of the ReportSubmitter gRPC service that handles the submission of reports.
/// </summary>
public sealed class ReportServiceImpl : ReportSubmitter.ReportSubmitterBase
{
    
    #region Fields
    
    private readonly ILogger<ReportServiceImpl> _logger;
    private readonly IValidator<ReportRequest> _validator;
    private readonly IRepository<ReportEntity> _reportRepository;
    private readonly IRepository<AuthorEntity> _authorRepository;
    
    #endregion
    
    #region Constructor
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ReportServiceImpl"/> class.
    /// Injects the necessary dependencies including a logger, validator, and repositories for handling reports and authors.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="validator"></param>
    /// <param name="reportRepository"></param>
    /// <param name="authorRepository"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ReportServiceImpl(
        ILogger<ReportServiceImpl> logger,
        IValidator<ReportRequest> validator,
        IRepository<ReportEntity> reportRepository,
        IRepository<AuthorEntity> authorRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
    }
    
    #endregion
    
    #region Methods
    
    /// <summary>
    /// Creates a <see cref="ReportResponse"/> object based on the operation's success status and a message.
    /// </summary>
    /// <param name="success">Indicates whether the operation was successful.</param>
    /// <param name="message">A message describing the result of the operation.</param>
    /// <returns>A <see cref="ReportResponse"/> object with the success status and message.</returns>
    private static ReportResponse CreateReportResponse(
        bool success,
        string message)
    {
        return new ReportResponse
        {
            Success = success,
            Message = message
        };
    }

    /// <summary>
    /// Creates an <see cref="AuthorEntity"/> object with the provided details.
    /// </summary>
    /// <param name="id">The author's unique identifier.</param>
    /// <param name="firstName">The author's first name.</param>
    /// <param name="lastName">The author's last name.</param>
    /// <param name="email">The author's email address.</param>
    /// <returns>An <see cref="AuthorEntity"/> object populated with the provided data.</returns>
    private static AuthorEntity CreateAuthor(
        long id,
        string firstName,
        string lastName,
        string email)
    {
        return new AuthorEntity 
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
    }
    
    #endregion
    
    #region ReportService.GrpcServiceReportSubmitter.ReportSubmitterBase overrides
    
    /// <summary>
    /// Handles the submission of a report by validating the request, creating or retrieving the author entity, and saving the report to the repository.
    /// In case of validation errors or exceptions, logs the errors and returns a failure response.
    /// </summary>
    /// <param name="request">The <see cref="ReportRequest"/> containing the report data.</param>
    /// <param name="context">The gRPC server call context.</param>
    /// <returns>A <see cref="ReportResponse"/> indicating the success or failure of the operation.</returns>
    public override async Task<ReportResponse> SubmitReport(
        ReportRequest request,
        ServerCallContext context)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(error => error.ErrorMessage));
            _logger.LogError("Validation failed: {Errors}", errors);
            
            return CreateReportResponse(false, "Validation failed: " + errors);
        }
        
        _logger.LogInformation("Attempting to submit report.");

        try
        {
            var entity = CreateAuthor(
                request.Metadata.Author.Id,
                request.Metadata.Author.FirstName,
                request.Metadata.Author.LastName,
                request.Metadata.Author.Email);

            var author = await _authorRepository.Get(entity);

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
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, "KeyNotFoundException: {ExceptionMessage}", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception: {ExceptionMessage}", ex.Message);
        }
        
        return CreateReportResponse(false, "Failed to submit report.");
    }
    
    #endregion
}