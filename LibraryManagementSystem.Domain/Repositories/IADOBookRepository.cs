using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Repositories
{
    public interface IADOBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksWithCategoriesAsync(CancellationToken ct = default);
    }
}
