namespace HouseFinances.Application.Persons;

public record PersonDto(Guid Id, string Name, int Age);

public record CreatePersonCommand(string Name, int Age);

public record UpdatePersonCommand(string Name, int Age);
