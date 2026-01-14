using FluentValidation;
using CleanArchitecture.Advanced.Api.Domain.Entities;

namespace CleanArchitecture.Advanced.Api.Application.Validation
{
    public class LibraryValidator : AbstractValidator<Library>
    {
        public LibraryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Library name is required")
                .MaximumLength(100).WithMessage("Library name can't exceed 100 characters");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Library address is required");
        }
    }
}
