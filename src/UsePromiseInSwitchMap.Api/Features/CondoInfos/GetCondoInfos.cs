using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using UsePromiseInSwitchMap.Api.Core;
using UsePromiseInSwitchMap.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UsePromiseInSwitchMap.Api.Features
{
    public class GetCondoInfos
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<CondoInfoDto> CondoInfos { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IUsePromiseInSwitchMapDbContext _context;
        
            public Handler(IUsePromiseInSwitchMapDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    CondoInfos = await _context.CondoInfos.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
