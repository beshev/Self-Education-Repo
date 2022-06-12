namespace DemoWebApi.Controllers
{
    using DemoLibrary.Commands;
    using DemoLibrary.Models;
    using DemoLibrary.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<PersonModel>> Get()
        {
            return await _mediator.Send(new GetPersonListQuery());
        }

        [HttpGet("{id}")]
        public async Task<PersonModel> Get(int id)
        {
            return await _mediator.Send(new GetPersonByIdQuery(id));
        }

        [HttpPost]
        public async Task<PersonModel> Post([FromBody] PersonModel model)
        {
            return await _mediator.Send(new InsertPersonCommand(model.FirstName, model.LastName));
        }

        [HttpDelete("{id}")]
        public async Task<PersonModel> Delete(int id)
        {
            return await _mediator.Send(new DeletePersonCommand(id));
        }
    }
}
