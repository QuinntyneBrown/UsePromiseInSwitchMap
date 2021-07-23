using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using UsePromiseInSwitchMap.Api.Models;
using UsePromiseInSwitchMap.Api.Core;
using UsePromiseInSwitchMap.Api.Interfaces;

namespace UsePromiseInSwitchMap.Api.Features
{
    public class RemoveCondoInfo
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
                var condoInfo = await _context.CondoInfos.SingleAsync(x => x.CondoInfoId == request.CondoInfoId);
                
                _context.CondoInfos.Remove(condoInfo);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    CondoInfo = condoInfo.ToDto()
                };
            }
            
        }
    }
}
