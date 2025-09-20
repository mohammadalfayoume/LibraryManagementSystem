using LibraryManagementSystem.Application.Mappings;

namespace LibraryManagementSystem.Application.DTOs.Category
{
    public class UpdateCategoryDto : IMapFrom<Domain.Entities.Category>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
