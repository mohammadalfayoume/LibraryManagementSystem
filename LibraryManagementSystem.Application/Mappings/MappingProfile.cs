using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.DTOs.Book;
using LibraryManagementSystem.Application.DTOs.Category;
using LibraryManagementSystem.Domain.Entities;
using System.Reflection;

namespace LibraryManagementSystem.Application.Mappings
{
    //public class MappingProfile : Profile
    //{
    //    public MappingProfile()
    //    {
    //        // Book → BookDto
    //        CreateMap<BookDto, Book>();


    //        // Category → CategoryDto
    //        CreateMap<CategoryDto, Category>();

    //        // Book -> BookBrief
    //        CreateMap<Book, BookBrief>()
    //            .ForMember(dest => dest.BookCategories,
    //                opt => opt.MapFrom(src => src.BookCategories.Select(bc => bc.Category)));

    //        // Category -> CategoryBrief
    //        CreateMap<Category, CategoryBrief>();

    //        // Reverse Maps
    //        CreateMap<BookDto, Book>();
    //        CreateMap<CategoryDto, Category>();
    //    }
    //}

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);

            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            interfaceMethodInfo?.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }
}
