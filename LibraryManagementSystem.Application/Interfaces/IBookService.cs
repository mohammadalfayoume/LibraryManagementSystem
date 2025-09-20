using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.DTOs.Book;
using LibraryManagementSystem.Application.Shared;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookBrief> CreateAsync(CreateBookDto book, CancellationToken ct = default);
        Task UpdateAsync(UpdateBookDto book, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<PagedResult<BookBrief>> GetAllWithCategoriesPagedAsync(PagedDto request, CancellationToken ct = default);
        Task<BookBrief> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsNameExistAsync(string name, CancellationToken ct = default);
        Task<bool> IsNameExistByIdAsync(Guid id, string name, CancellationToken ct);
    }
}
