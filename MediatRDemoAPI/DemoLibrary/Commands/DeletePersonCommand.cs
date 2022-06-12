namespace DemoLibrary.Commands
{
    using DemoLibrary.Models;
    using MediatR;

    public record DeletePersonCommand(int Id) : IRequest<PersonModel>;
}
