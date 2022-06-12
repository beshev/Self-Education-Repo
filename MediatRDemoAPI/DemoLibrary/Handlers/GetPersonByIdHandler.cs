namespace DemoLibrary.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using DemoLibrary.Models;
    using DemoLibrary.Queries;
    using MediatR;

    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonModel>
    {
        private readonly IMediator _mediator;

        public GetPersonByIdHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task<PersonModel> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            // This is an example to see how we can combine mediator queries.
            var people = await _mediator.Send(new GetPersonListQuery());

            return people.FirstOrDefault(x => x.Id == request.id);
        }
    }
}
