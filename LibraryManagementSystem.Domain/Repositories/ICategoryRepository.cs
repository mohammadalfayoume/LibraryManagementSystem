using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Category>> ListAsync(CancellationToken ct = default);
        Task<bool> IsCategoryNameExistAsync(string name, CancellationToken ct = default);
        Task AddAsync(Category category, CancellationToken ct = default);
        Task UpdateAsync(Category category, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsCategoryNameExistByIdAsync(Guid id, string name, CancellationToken ct);
        Task<bool> IsAttachedWithBookAsync(Guid id, CancellationToken ct);
    }
}
