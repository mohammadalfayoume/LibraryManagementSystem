using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.Id)
                .IsRequired(true)
                .ValueGeneratedNever()
                .HasDefaultValueSql("NEWID()");

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

            builder.Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired(true);

            builder.HasMany(x => x.BookCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
