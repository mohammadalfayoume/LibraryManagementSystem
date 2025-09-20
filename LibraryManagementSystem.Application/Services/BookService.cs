using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.DTOs.Book;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Mappings;
using LibraryManagementSystem.Application.Shared;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;

namespace LibraryManagementSystem.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IADOBookRepository _aDOBookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IADOBookRepository aDOBookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _aDOBookRepository = aDOBookRepository;
            _mapper = mapper;
        }
        public async Task<BookBrief> CreateAsync(CreateBookDto bookDto, CancellationToken ct = default)
        {
            DateTime now = DateTime.Now;
            var book = new Book();
            book.Name = bookDto.Name;
            book.Id = Guid.NewGuid();
            book.CreatedAt = now;
            foreach (var cat in bookDto.CategoryIds)
            {
                book.BookCategories.Add(new BookCategory
                {
                    BookId = book.Id,
                    CategoryId = cat,
                    CreatedAt = now,
                });
            }

            await _bookRepository.AddAsync(book, ct);

            return new BookBrief
            {
                Id = book.Id,
                Name = bookDto.Name,
            };
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            await _bookRepository.DeleteAsync(id, ct);
        }

        public async Task<PagedResult<BookBrief>> GetAllWithCategoriesPagedAsync(PagedDto request, CancellationToken ct = default)
        {
            var books = await _aDOBookRepository.GetAllBooksWithCategoriesAsync(ct);

            var booksToReturn = books.Select(b => new BookBrief
            {
                Id = b.Id,
                Name = b.Name,
                BookCategories = b.BookCategories.Select(bc => new CategoryBrief
                {
                    Id = bc.CategoryId,
                    Name = bc.Category.Name
                })
            }).ToList();

            return booksToReturn.ToPagedResult(request.PageNumber, request.PageSize);
        }

        public async Task<BookBrief> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var book = await _bookRepository.GetByIdAsync(id, ct);

            if (book is null) return null;

            return _mapper.Map<BookBrief>(book);

        }

        public async Task<bool> IsNameExistAsync(string name, CancellationToken ct = default)
        {
            return await _bookRepository.IsBookNameExistAsync(name, ct);
        }

        public async Task<bool> IsNameExistByIdAsync(Guid id, string name, CancellationToken ct)
        {
            return await _bookRepository.IsBookNameExistByIdAsync(id, name, ct);
        }

        public async Task UpdateAsync(UpdateBookDto bookDto, CancellationToken ct = default)
        {
            var book = await GetBook(bookDto.Id, ct);

            book.BookCategories.Clear();

            DateTime now = DateTime.Now;

            foreach (var cat in bookDto.CategoryIds)
            {
                book.BookCategories.Add(new BookCategory
                {
                    BookId = book.Id,
                    CategoryId = cat,
                    CreatedAt = now,
                });
            }
            book.ModifiedAt = now;
            book.Name = bookDto.Name;

            

            await _bookRepository.UpdateAsync(book, ct);
        }

        private async Task<Book?> GetBook(Guid id, CancellationToken ct)
        {
            return await _bookRepository.GetByIdAsync(id, ct);
        }
    }
}
