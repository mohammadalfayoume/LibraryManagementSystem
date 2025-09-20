using LibraryManagementSystem.Application.Mappings;

namespace LibraryManagementSystem.Application.DTOs.Category
{
    public class CreateCategoryDto : IMapFrom<Domain.Entities.Category>
    {
        public string Name { get; set; }
    }
}
