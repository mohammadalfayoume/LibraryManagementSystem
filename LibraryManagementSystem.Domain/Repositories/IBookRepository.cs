using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsBookNameExistAsync(string name, CancellationToken ct = default);
        Task<IEnumerable<Book>> ListAsync(CancellationToken ct = default);
        Task AddAsync(Book book, CancellationToken ct = default);
        Task UpdateAsync(Book book, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsBookNameExistByIdAsync(Guid id, string name, CancellationToken ct);
    }
}
