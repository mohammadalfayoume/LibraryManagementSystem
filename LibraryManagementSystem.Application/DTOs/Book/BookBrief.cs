using LibraryManagementSystem.Application.Mappings;

namespace LibraryManagementSystem.Application.DTOs
{
    public class BookBrief : IMapFrom<Domain.Entities.Book>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryBrief> BookCategories { get; set; }
    }
}
