using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsePromiseInSwitchMap.Api.Core;
using UsePromiseInSwitchMap.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UsePromiseInSwitchMap.Api.Features
{
    public class GetCondoInfoById
    {
        public class Request: IRequest<Response>
        {
            public Guid CondoInfoId { get; set; }
        }

        public class Response: ResponseBase
        {
            public CondoInfoDto CondoInfo { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IUsePromiseInSwitchMapDbContext _context;
        
            public Handler(IUsePromiseInSwitchMapDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    CondoInfo = (await _context.CondoInfos.SingleOrDefaultAsync(x => x.CondoInfoId == request.CondoInfoId)).ToDto()
                };
            }
            
        }
    }
}
