namespace DemoLibrary.Queries
{
    using DemoLibrary.Models;
    using MediatR;

    public record GetPersonByIdQuery(int id) : IRequest<PersonModel>;
}
