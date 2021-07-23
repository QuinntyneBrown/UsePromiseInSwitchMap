using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsePromiseInSwitchMap.Api.Core;
using UsePromiseInSwitchMap.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UsePromiseInSwitchMap.Api.Features
{
    public class GetAddressById
    {
        public class Request: IRequest<Response>
        {
            public Guid AddressId { get; set; }
        }

        public class Response: ResponseBase
        {
            public AddressDto Address { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IUsePromiseInSwitchMapDbContext _context;
        
            public Handler(IUsePromiseInSwitchMapDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Address = (await _context.Addresses.SingleOrDefaultAsync(x => x.AddressId == request.AddressId)).ToDto()
                };
            }
            
        }
    }
}
