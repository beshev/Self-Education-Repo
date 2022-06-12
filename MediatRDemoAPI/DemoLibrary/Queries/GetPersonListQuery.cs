namespace DemoLibrary.Queries
{
    using DemoLibrary.Models;
    using MediatR;

    public record GetPersonListQuery() : IRequest<IEnumerable<PersonModel>>;
}
