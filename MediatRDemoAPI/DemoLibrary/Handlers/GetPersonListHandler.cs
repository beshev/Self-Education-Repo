namespace DemoLibrary.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
 
    using DemoLibrary.DataAccess;
    using DemoLibrary.Models;
    using DemoLibrary.Queries;
    using MediatR;

    internal class GetPersonListHandler : IRequestHandler<GetPersonListQuery, IEnumerable<PersonModel>>
    {
        private readonly IDataAccess _data;

        public GetPersonListHandler(IDataAccess data)
        {
            this._data = data;
        }

        public Task<IEnumerable<PersonModel>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.GetPeople());
        }
    }
}
