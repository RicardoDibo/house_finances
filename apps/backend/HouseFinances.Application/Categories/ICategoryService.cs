namespace HouseFinances.Application.Categories;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync();
    Task<CategoryDto> CreateAsync(CreateCategoryCommand command);
}
