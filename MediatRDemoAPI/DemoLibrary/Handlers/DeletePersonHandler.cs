namespace DemoLibrary.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using DemoLibrary.Commands;
    using DemoLibrary.DataAccess;
    using DemoLibrary.Models;
    using MediatR;
    
    public class DeletePersonHandler : IRequestHandler<DeletePersonCommand, PersonModel>
    {
        private readonly IDataAccess _data;

        public DeletePersonHandler(IDataAccess data)
        {
            this._data = data;
        }

        public Task<PersonModel> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.DeletePerson(request.Id));
        }
    }
}
