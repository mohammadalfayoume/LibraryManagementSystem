using LibraryManagementSystem.Application.Mappings;

namespace LibraryManagementSystem.Application.DTOs.Book
{
    public class UpdateBookDto : IMapFrom<Domain.Entities.Book>
    {
        public UpdateBookDto()
        {
            CategoryIds = new List<Guid>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}
