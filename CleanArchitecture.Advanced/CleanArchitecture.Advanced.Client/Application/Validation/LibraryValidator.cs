using FluentValidation;
using CleanArchitecture.Advanced.Client.Domain.Models;

namespace CleanArchitecture.Advanced.Client.Application.Validation
{
    public class LibraryValidator : AbstractValidator<LibraryModel>
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
