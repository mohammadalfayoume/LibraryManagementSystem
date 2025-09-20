using LibraryManagementSystem.Application.DTOs.Category;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Shared;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using LibraryManagementSystem.Application.DTOs.Book;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// Get all categories pagenated.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult<CategoryBrief>>> GetAllPaged([FromQuery] PagedDto request, CancellationToken ct)
        {
            var categories = await _categoryService.GetAllPagedAsync(request, ct);
            return Ok(categories);
        }

        /// <summary>
        /// Get all categories for dropdown list.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryBrief>>> GetAll(CancellationToken ct)
        {
            var categories = await _categoryService.GetAllAsync(ct);
            return Ok(categories);
        }


        /// <summary>
        /// Get a category by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryBrief>> GetById(Guid id, CancellationToken ct)
        {
            var category = await _categoryService.GetByIdAsync(id, ct);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CategoryBrief>> Create([FromBody] CreateCategoryDto categoryDto, CancellationToken ct, [FromServices] IValidator<CreateCategoryDto> validator)
        {

            var validationResult = await validator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var createdCategory = await _categoryService.CreateAsync(categoryDto, ct);

            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        /// <summary>
        /// Update an existing category.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDto categoryDto, CancellationToken ct, [FromServices] IValidator<UpdateCategoryDto> validator)
        {
            var validationResult = await validator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _categoryService.UpdateAsync(categoryDto, ct);

            return NoContent();
        }

        /// <summary>
        /// Delete a category by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var category = await _categoryService.GetByIdAsync(id, ct);
            if (category == null)
                return NotFound();

            await _categoryService.DeleteAsync(id, ct);

            return NoContent();
        }
    }
}
