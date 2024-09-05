using FluentValidation;
using ReportService.Grpc;

namespace ReportService.Proto.Validators;

/// <summary>
/// Validator class for validating properties of the <see cref="ReportRequest"/> object.
/// </summary>
public sealed class ReportRequestValidator : AbstractValidator<ReportRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReportRequestValidator"/> class and defines validation rules for the <see cref="ReportRequest"/> object.
    /// Validates that the Metadata is not null and passes additional validation using <see cref="MetadataValidator"/>, and that the WordFileUrl is not empty and is a valid URL.
    /// </summary>
    public ReportRequestValidator()
    {
        RuleFor(r => r.Metadata)
            .NotNull().WithMessage("Metadata is required")
            .SetValidator(new MetadataValidator());
        
        RuleFor(r => r.WordFileUrl)
            .NotEmpty().WithMessage("Word file url is required")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Word file url must be a valid URL");
    }
}