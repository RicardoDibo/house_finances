namespace HouseFinances.Application.Persons;

public interface IPersonService
{
    Task<IReadOnlyList<PersonDto>> GetAllAsync();
    Task<PersonDto?> GetByIdAsync(Guid id);
    Task<PersonDto> CreateAsync(CreatePersonCommand command);
    Task<PersonDto> UpdateAsync(Guid id, UpdatePersonCommand command);
    Task DeleteAsync(Guid id);
}
