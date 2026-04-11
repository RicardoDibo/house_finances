using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Enums;
using HouseFinances.Domain.Exceptions;
using HouseFinances.Domain.Repositories;

namespace HouseFinances.Application.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository) => _repository = repository;

    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(ToDto).ToList();
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryCommand command)
    {
        if (!Enum.IsDefined(typeof(CategoryPurpose), command.Purpose))
            throw new DomainException("Invalid purpose. Use 0 (Expense), 1 (Income) or 2 (Both).");

        var purpose = (CategoryPurpose)command.Purpose;
        var category = Category.Create(command.Description, purpose);
        _repository.Add(category);
        await _repository.SaveChangesAsync();
        return ToDto(category);
    }

    private static CategoryDto ToDto(Category c) =>
        new(c.Id, c.Description, (int)c.Purpose, c.Purpose.ToString());
}
