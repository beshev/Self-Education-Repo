namespace DemoLibrary.Commands
{
    using DemoLibrary.Models;
    using MediatR;

    public record InsertPersonCommand(string FirstName, string LastName) : IRequest<PersonModel>;
}
