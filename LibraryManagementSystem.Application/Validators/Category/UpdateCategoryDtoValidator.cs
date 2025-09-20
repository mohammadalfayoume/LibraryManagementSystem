using FluentValidation;
using LibraryManagementSystem.Application.DTOs.Category;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Domain.ValueObjects;

namespace LibraryManagementSystem.Application.Validators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryDtoValidator(ICategoryService categoryService)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(string.Format(Localization.REQUIRED_FIELD_ERROR_EN, "Category"));

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(Localization.REQUIRED_FIELD_ERROR_EN, "Category Name"))
                .MaximumLength(250)
                .WithMessage(string.Format(Localization.MAX_LENGTH_ERROR_EN, "Category Name", "250"))
                .MustAsync(async (command, name, cancellation) => !await IsCategoryNameExist(command.Id, name, cancellation))
                .WithMessage(string.Format(Localization.ALREADY_EXISTS_EN, "Category Name"));

            _categoryService = categoryService;
        }

        private async Task<bool> IsCategoryNameExist(Guid id, string name, CancellationToken ct)
        {
            return await _categoryService.IsNameExistByIdAsync(id, name, ct);
        }
    }
}
