using System.Net;
using System.Threading.Tasks;
using UsePromiseInSwitchMap.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UsePromiseInSwitchMap.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CondoInfoController
    {
        private readonly IMediator _mediator;

        public CondoInfoController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{condoInfoId}", Name = "GetCondoInfoByIdRoute")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCondoInfoById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCondoInfoById.Response>> GetById([FromRoute]GetCondoInfoById.Request request)
        {
            var response = await _mediator.Send(request);
        
            if (response.CondoInfo == null)
            {
                return new NotFoundObjectResult(request.CondoInfoId);
            }
        
            return response;
        }
        
        [HttpGet(Name = "GetCondoInfosRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCondoInfos.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCondoInfos.Response>> Get()
            => await _mediator.Send(new GetCondoInfos.Request());
        
        [HttpPost(Name = "CreateCondoInfoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateCondoInfo.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateCondoInfo.Response>> Create([FromBody]CreateCondoInfo.Request request)
            => await _mediator.Send(request);
        
        [HttpGet("page/{pageSize}/{index}", Name = "GetCondoInfosPageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCondoInfosPage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCondoInfosPage.Response>> Page([FromRoute]GetCondoInfosPage.Request request)
            => await _mediator.Send(request);
        
        [HttpPut(Name = "UpdateCondoInfoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateCondoInfo.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateCondoInfo.Response>> Update([FromBody]UpdateCondoInfo.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{condoInfoId}", Name = "RemoveCondoInfoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveCondoInfo.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveCondoInfo.Response>> Remove([FromRoute]RemoveCondoInfo.Request request)
            => await _mediator.Send(request);
        
    }
}
