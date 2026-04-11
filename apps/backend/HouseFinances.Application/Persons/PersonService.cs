using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Repositories;

namespace HouseFinances.Application.Persons;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;

    public PersonService(IPersonRepository repository) => _repository = repository;

    public async Task<IReadOnlyList<PersonDto>> GetAllAsync()
    {
        var persons = await _repository.GetAllAsync();
        return persons.Select(p => new PersonDto(p.Id, p.Name, p.Age)).ToList();
    }

    public async Task<PersonDto?> GetByIdAsync(Guid id)
    {
        var person = await _repository.GetByIdAsync(id);
        return person is null ? null : new PersonDto(person.Id, person.Name, person.Age);
    }

    public async Task<PersonDto> CreateAsync(CreatePersonCommand command)
    {
        var person = Person.Create(command.Name, command.Age);
        _repository.Add(person);
        await _repository.SaveChangesAsync();
        return new PersonDto(person.Id, person.Name, person.Age);
    }

    public async Task<PersonDto> UpdateAsync(Guid id, UpdatePersonCommand command)
    {
        var person = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Person {id} not found.");
        person.Update(command.Name, command.Age);
        await _repository.SaveChangesAsync();
        return new PersonDto(person.Id, person.Name, person.Age);
    }

    public async Task DeleteAsync(Guid id)
    {
        var person = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Person {id} not found.");
        _repository.Remove(person);
        await _repository.SaveChangesAsync();
    }
}
