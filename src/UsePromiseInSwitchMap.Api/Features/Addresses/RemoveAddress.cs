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
    public class RemoveAddress
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
                var address = await _context.Addresses.SingleAsync(x => x.AddressId == request.AddressId);
                
                _context.Addresses.Remove(address);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Address = address.ToDto()
                };
            }
            
        }
    }
}
