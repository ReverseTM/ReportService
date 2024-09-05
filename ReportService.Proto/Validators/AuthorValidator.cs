using FluentValidation;
using ReportService.Grpc;

namespace ReportService.Proto.Validators;

/// <summary>
/// Validator class for validating properties of the <see cref="Author"/> object.
/// </summary>
public sealed class AuthorValidator : AbstractValidator<Author>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorValidator"/> class and defines validation rules for the <see cref="Author"/> object.
    /// Validates that the first name, last name, and email are not empty, that the first name and last name contain only letters, and that the email is in a valid format.
    /// </summary>
    public AuthorValidator()
    {
        RuleFor(a => a.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty")
            .Matches("^[a-zA-Z]+$").WithMessage("First name must contain only letters");
        
        RuleFor(a => a.LastName)
            .NotEmpty().WithMessage("Last name cannot be empty")
            .Matches("^[a-zA-Z]+$").WithMessage("Last name must contain only letters");
        
        RuleFor(a => a.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email must be a valid email address");
    }
}