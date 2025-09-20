using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.DTOs.Category;
using LibraryManagementSystem.Application.Extentions;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Mappings;
using LibraryManagementSystem.Application.Shared;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;

namespace LibraryManagementSystem.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categorycRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository CategoryRepository, IMapper mapper)
        {
            _categorycRepository = CategoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryBrief> CreateAsync(CreateCategoryDto categoryDto, CancellationToken ct = default)
        {
            DateTime now = DateTime.Now;
            var category = _mapper.Map<Category>(categoryDto);
            category.Id = Guid.NewGuid();
            category.CreatedAt = now;


            await _categorycRepository.AddAsync(category, ct);

            return _mapper.Map<CategoryBrief>(category);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            bool isAttached = await IsAttachedWithBookAsync(id, ct);

            if (isAttached)
            {
                throw new Application.Extentions.ApplicationException("Category attached with Books");
            }
            await _categorycRepository.DeleteAsync(id, ct);
        }

        private async Task<bool> IsAttachedWithBookAsync(Guid id, CancellationToken ct)
        {
            return await _categorycRepository.IsAttachedWithBookAsync(id, ct);
        }

        public async Task<IEnumerable<CategoryBrief>> GetAllAsync(CancellationToken ct = default)
        {
            var categories = await _categorycRepository.ListAsync(ct);

            return _mapper.Map<IEnumerable<CategoryBrief>>(categories);
        }

        public async Task<PagedResult<CategoryBrief>> GetAllPagedAsync(PagedDto request, CancellationToken ct = default)
        {
            var categories = await _categorycRepository.ListAsync(ct);

            var categorysToReturn = _mapper.Map<IEnumerable<CategoryBrief>>(categories);
            return categorysToReturn.ToPagedResult(request.PageNumber, request.PageSize);
        }


        public async Task<CategoryBrief> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var category = await _categorycRepository.GetByIdAsync(id, ct);

            if (category is null) return null;

            return _mapper.Map<CategoryBrief>(category);
        }

        public async Task<bool> IsNameExistAsync(string name, CancellationToken ct = default)
        {
            return await _categorycRepository.IsCategoryNameExistAsync(name, ct);
        }

        public async Task<bool> IsNameExistByIdAsync(Guid id, string name, CancellationToken ct)
        {
            return await _categorycRepository.IsCategoryNameExistByIdAsync(id, name, ct);
        }

        public async Task UpdateAsync(UpdateCategoryDto categoryDto, CancellationToken ct = default)
        {
            var category = await GetCategory(categoryDto.Id, ct);

            _mapper.Map(categoryDto, category);

            category.ModifiedAt = DateTime.UtcNow;

            await _categorycRepository.UpdateAsync(category, ct);
        }

        private async Task<Category?> GetCategory(Guid id, CancellationToken ct)
        {
            return await _categorycRepository.GetByIdAsync(id, ct);
        }

    }
}
