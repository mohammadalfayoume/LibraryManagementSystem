using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Category category, CancellationToken ct = default)
        {
            await _dbContext.Category.AddAsync(category, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Category category, CancellationToken ct = default)
        {
            _dbContext.Category.Update(category);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            Category? category = await _dbContext.Category.FindAsync(id);

            if (category is not null)
            {
                category.IsDeleted = true;
                _dbContext.Category.Update(category);
                await _dbContext.SaveChangesAsync(ct);
            }
        }

        public async Task<IEnumerable<Category>> ListAsync(CancellationToken ct = default)
        {
            var Categorys = await _dbContext.Category
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(ct);

            return Categorys;
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var category = await _dbContext.Category
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Book)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            return category;
        }

        public async Task<bool> IsCategoryNameExistAsync(string name, CancellationToken ct = default)
        {
            return await _dbContext.Category.AnyAsync(x => x.Name.Equals(name), ct);
        }

        public async Task<bool> IsCategoryNameExistByIdAsync(Guid id, string name, CancellationToken ct)
        {
            return await _dbContext.Category.AnyAsync(x => x.Id != id && x.Name.Equals(name), ct);
        }

        public async Task<bool> IsAttachedWithBookAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.BookCategory.AnyAsync(x => x.CategoryId == id);
        }
    }
}