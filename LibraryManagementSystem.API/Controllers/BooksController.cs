using FluentValidation;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.DTOs.Book;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        /// <summary>
        /// Get all books with their categories.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult<BookBrief>>> GetAll([FromQuery] PagedDto request, CancellationToken ct)
        {
            var books = await _bookService.GetAllWithCategoriesPagedAsync(request, ct);
            return Ok(books);
        }

        /// <summary>
        /// Get a book by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookBrief>> GetById(Guid id, CancellationToken ct)
        {
            var book = await _bookService.GetByIdAsync(id, ct);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        /// <summary>
        /// Create a new book.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BookBrief>> Create([FromBody] CreateBookDto bookDto, CancellationToken ct, [FromServices] IValidator<CreateBookDto> validator)
        {
            var validationResult = await validator.ValidateAsync(bookDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var createdBook = await _bookService.CreateAsync(bookDto, ct);

            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        /// <summary>
        /// Update an existing book.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateBookDto bookDto, CancellationToken ct, [FromServices] IValidator<UpdateBookDto> validator)
        {
            var validationResult = await validator.ValidateAsync(bookDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _bookService.UpdateAsync(bookDto, ct);

            return NoContent();
        }

        /// <summary>
        /// Delete a book by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var book = await _bookService.GetByIdAsync(id, ct);
            if (book == null)
                return NotFound();

            await _bookService.DeleteAsync(id, ct);

            return NoContent();
        }
    }
}
