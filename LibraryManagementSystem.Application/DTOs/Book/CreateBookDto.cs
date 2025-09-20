using LibraryManagementSystem.Application.Mappings;

namespace LibraryManagementSystem.Application.DTOs.Book
{
    public class CreateBookDto : IMapFrom<Domain.Entities.Book>
    {
        public CreateBookDto()
        {
            CategoryIds = new List<Guid>();
        }
        public string Name { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}
