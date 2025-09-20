using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly string _connectionString;

        public BookRepository(ApplicationDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public async Task AddAsync(Book book, CancellationToken ct = default)
        {
            await _dbContext.Book.AddAsync(book, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Book book, CancellationToken ct = default)
        {
            _dbContext.Book.Update(book);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            Book? book = await _dbContext.Book.FindAsync(id);

            if (book is not null)
            {
                book.IsDeleted = true;
                _dbContext.Book.Update(book);
                await _dbContext.SaveChangesAsync(ct);
            }
        }

        public async Task<IEnumerable<Book>> ListAsync(CancellationToken ct = default)
        {
            var books = await _dbContext.Book
                .AsNoTracking()
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .ToListAsync(ct);

            return books;
        }

        public async Task<Book?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var book = await _dbContext.Book
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            return book;
        }

        public async Task<bool> IsBookNameExistAsync(string name, CancellationToken ct = default)
        {
            return await _dbContext.Book.AnyAsync(x => x.Name.Equals(name), ct);
        }

        public async Task<bool> IsBookNameExistByIdAsync(Guid id, string name, CancellationToken ct)
        {
            return await _dbContext.Book.AnyAsync(x => x.Id != id && x.Name.Equals(name), ct);
        }
    }
}