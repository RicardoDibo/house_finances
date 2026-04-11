namespace HouseFinances.Application.Categories;

public record CategoryDto(Guid Id, string Description, int Purpose, string PurposeName);

public record CreateCategoryCommand(string Description, int Purpose);
