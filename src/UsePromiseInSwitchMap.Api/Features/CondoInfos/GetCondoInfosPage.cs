using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UsePromiseInSwitchMap.Api.Extensions;
using UsePromiseInSwitchMap.Api.Core;
using UsePromiseInSwitchMap.Api.Interfaces;
using UsePromiseInSwitchMap.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace UsePromiseInSwitchMap.Api.Features
{
    public class GetCondoInfosPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<CondoInfoDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IUsePromiseInSwitchMapDbContext _context;
        
            public Handler(IUsePromiseInSwitchMapDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from condoInfo in _context.CondoInfos
                    select condoInfo;
                
                var length = await _context.CondoInfos.CountAsync();
                
                var condoInfos = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = condoInfos
                };
            }
            
        }
    }
}
