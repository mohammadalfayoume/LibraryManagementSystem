using FluentValidation;
using LibraryManagementSystem.Application.DTOs.Book;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.ValueObjects;

namespace LibraryManagementSystem.Application.Validators
{
    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        private readonly IBookService _bookService;

        public UpdateBookDtoValidator(IBookService bookService)
        {
            _bookService = bookService;

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(string.Format(Localization.REQUIRED_FIELD_ERROR_EN, "Book"));

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(Localization.REQUIRED_FIELD_ERROR_EN, "Book Name"))
                .MaximumLength(250)
                .WithMessage(string.Format(Localization.MAX_LENGTH_ERROR_EN, "Book Name", "250"))
                .MustAsync(async (command, name, cancellation) => !await IsBookNameExist(command.Id, name, cancellation))
                .WithMessage(string.Format(Localization.ALREADY_EXISTS_EN, "Book Name"));

            RuleFor(x => x.CategoryIds)
                .NotEmpty()
                .WithMessage(string.Format(Localization.AT_LEAST_ONE_ITEM_ERROR_EN, "Category"));

        }

        private async Task<bool> IsBookNameExist(Guid id, string name, CancellationToken ct)
        {
            return await _bookService.IsNameExistByIdAsync(id, name, ct);
        }
    }
}
