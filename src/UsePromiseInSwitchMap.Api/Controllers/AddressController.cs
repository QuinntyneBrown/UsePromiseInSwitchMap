using System.Net;
using System.Threading.Tasks;
using UsePromiseInSwitchMap.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UsePromiseInSwitchMap.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{addressId}", Name = "GetAddressByIdRoute")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetAddressById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAddressById.Response>> GetById([FromRoute]GetAddressById.Request request)
        {
            var response = await _mediator.Send(request);
        
            if (response.Address == null)
            {
                return new NotFoundObjectResult(request.AddressId);
            }
        
            return response;
        }
        
        [HttpGet(Name = "GetAddressesRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetAddresses.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAddresses.Response>> Get()
            => await _mediator.Send(new GetAddresses.Request());
        
        [HttpPost(Name = "CreateAddressRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateAddress.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateAddress.Response>> Create([FromBody]CreateAddress.Request request)
            => await _mediator.Send(request);
        
        [HttpGet("page/{pageSize}/{index}", Name = "GetAddressesPageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetAddressesPage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetAddressesPage.Response>> Page([FromRoute]GetAddressesPage.Request request)
            => await _mediator.Send(request);
        
        [HttpPut(Name = "UpdateAddressRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateAddress.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateAddress.Response>> Update([FromBody]UpdateAddress.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{addressId}", Name = "RemoveAddressRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveAddress.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveAddress.Response>> Remove([FromRoute]RemoveAddress.Request request)
            => await _mediator.Send(request);
        
    }
}
