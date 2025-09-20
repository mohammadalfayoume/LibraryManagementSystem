using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class ADOBookRepository : IADOBookRepository
    {
        private readonly string _connectionString;

        public ADOBookRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Book>> GetAllBooksWithCategoriesAsync(CancellationToken ct = default)
        {
            var books = new Dictionary<Guid, Book>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("GetAllBooksWithCategories", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync(ct);
                using (var reader = await cmd.ExecuteReaderAsync(ct))
                {
                    if (!reader.HasRows)
                    {
                        
                    }
                    while (await reader.ReadAsync(ct))
                    {
                        var bookId = reader.GetGuid(reader.GetOrdinal("BookId"));

                        var bookNameOrdinal = reader.GetOrdinal("BookName");
                        var bookName = reader.IsDBNull(bookNameOrdinal) ? null : reader.GetString(bookNameOrdinal);

                        var categoryIdOrdinal = reader.GetOrdinal("CategoryId");
                        Guid? categoryId = reader.IsDBNull(categoryIdOrdinal) ? (Guid?)null : reader.GetGuid(categoryIdOrdinal);

                        var categoryNameOrdinal = reader.GetOrdinal("CategoryName");
                        var categoryName = reader.IsDBNull(categoryNameOrdinal) ? null : reader.GetString(categoryNameOrdinal);

                        if (!books.TryGetValue(bookId, out var book))
                        {
                            book = new Book
                            {
                                Id = bookId,
                                Name = bookName,
                                BookCategories = new List<BookCategory>()
                            };
                            books.Add(bookId, book);
                        }

                        if (categoryId.HasValue)
                        {
                            var category = new Category
                            {
                                Id = categoryId.Value,
                                Name = categoryName
                            };

                            book.BookCategories.Add(new BookCategory
                            {
                                BookId = bookId,
                                CategoryId = categoryId.Value,
                                Category = category
                            });
                        }
                        
                    }
                }
            }

            return books.Values.ToList();
        }


    }
}

/*
 CREATE PROCEDURE [dbo].[GetAllBooksWithCategories]
AS
BEGIN
    SELECT 
        b.Id AS BookId,
        b.Name AS BookName,
        c.Id AS CategoryId,
        c.Name AS CategoryName
    FROM Books b
    INNER JOIN BookCategories bc ON b.Id = bc.BookId
    INNER JOIN Categories c ON bc.CategoryId = c.Id
END
 */
