using FluentValidation;
using ReportService.Grpc;

namespace ReportService.Proto.Validators;

/// <summary>
/// Validator class for validating properties of the <see cref="Metadata"/> object.
/// </summary>
public sealed class MetadataValidator : AbstractValidator<Metadata>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MetadataValidator"/> class and defines validation rules for the <see cref="Metadata"/> object.
    /// Validates that the Author is not null and passes additional validation using <see cref="AuthorValidator"/>, that the RequestTime is not empty, 
    /// and that ReferencedFiles is not null and contains only valid URLs.
    /// </summary>
    public MetadataValidator()
    {
        RuleFor(m => m.Author)
            .NotNull().WithMessage("Author is required")
            .SetValidator(new AuthorValidator());
        
        RuleFor(m => m.RequestTime)
            .NotEmpty().WithMessage("RequestTime is required");
        
        RuleFor(m => m.ReferencedFiles)
            .NotNull().WithMessage("ReferencedFiles must not be empty")
            .Must(files => files.All(file => Uri.TryCreate(file, UriKind.Absolute, out _))).WithMessage("All ReferencedFiles must be valid URLs");
    }
}