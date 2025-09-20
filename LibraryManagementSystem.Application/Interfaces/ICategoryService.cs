using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.DTOs.Category;
using LibraryManagementSystem.Application.Shared;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryBrief> CreateAsync(CreateCategoryDto category, CancellationToken ct = default);
        Task UpdateAsync(UpdateCategoryDto category, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
        Task<PagedResult<CategoryBrief>> GetAllPagedAsync(PagedDto request, CancellationToken ct = default);
        Task<IEnumerable<CategoryBrief>> GetAllAsync(CancellationToken ct = default);
        Task<CategoryBrief> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> IsNameExistAsync(string name, CancellationToken ct = default);
        Task<bool> IsNameExistByIdAsync(Guid id, string name, CancellationToken ct);
    }
}
