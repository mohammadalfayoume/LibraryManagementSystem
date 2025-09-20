using FluentValidation;
using LibraryManagementSystem.Application.DTOs.Category;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Domain.ValueObjects;

namespace LibraryManagementSystem.Application.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryDtoValidator(ICategoryService categoryService)
        {
            _categoryService = categoryService;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(Localization.REQUIRED_FIELD_ERROR_EN, "Category Name"))
                .MaximumLength(250)
                .WithMessage(string.Format(Localization.MAX_LENGTH_ERROR_EN, "Category Name", "250"))
                .MustAsync(async (name, cancellation) => !await IsCategoryNameExist(name, cancellation))
                .WithMessage(string.Format(Localization.ALREADY_EXISTS_EN, "Category Name"));

        }

        private async Task<bool> IsCategoryNameExist(string name, CancellationToken ct)
        {
            return await _categoryService.IsNameExistAsync(name, ct);
        }
    }
}
