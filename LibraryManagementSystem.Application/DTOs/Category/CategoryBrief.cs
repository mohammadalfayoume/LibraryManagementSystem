using LibraryManagementSystem.Application.Mappings;

namespace LibraryManagementSystem.Application.DTOs
{
    public class CategoryBrief : IMapFrom<Domain.Entities.Category>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
