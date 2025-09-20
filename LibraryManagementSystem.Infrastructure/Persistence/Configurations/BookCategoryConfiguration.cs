using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(bc => new {bc.BookId, bc.CategoryId});

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.CreatedAt)
                .IsRequired(true)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.ModifiedAt)
                .IsRequired(false)
                .HasColumnType("datetime");

            builder.Property(x => x.CreatedBy)
                .IsRequired(true);

            builder.Property(x => x.ModifiedBy)
                .IsRequired(false);

            builder.Property(x => x.IsDeleted)
                .IsRequired(true)
                .HasDefaultValueSql("0");

        }
    }
}
