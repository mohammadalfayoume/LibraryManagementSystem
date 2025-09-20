using FluentValidation;
using LibraryManagementSystem.Application.DTOs.Book;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.ValueObjects;

namespace LibraryManagementSystem.Application.Validators
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        private readonly IBookService _bookService;

        public CreateBookDtoValidator(IBookService bookService)
        {
            _bookService = bookService;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(Localization.REQUIRED_FIELD_ERROR_EN, "Book Name"))
                .MaximumLength(250)
                .WithMessage(string.Format(Localization.MAX_LENGTH_ERROR_EN, "Book Name", "250"))
                .MustAsync(async (name, cancellation) => !await IsBookNameExist(name, cancellation))
                .WithMessage(string.Format(Localization.ALREADY_EXISTS_EN, "Book Name"));

            RuleFor(x => x.CategoryIds)
                .NotEmpty()
                .WithMessage(string.Format(Localization.AT_LEAST_ONE_ITEM_ERROR_EN, "Category"));

        }

        private async Task<bool> IsBookNameExist(string name, CancellationToken ct)
        {
            return await _bookService.IsNameExistAsync(name, ct);
        }
    }
}
