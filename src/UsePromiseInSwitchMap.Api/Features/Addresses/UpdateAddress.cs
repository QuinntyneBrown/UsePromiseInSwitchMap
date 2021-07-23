using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UsePromiseInSwitchMap.Api.Core;
using UsePromiseInSwitchMap.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UsePromiseInSwitchMap.Api.Features
{
    public class UpdateAddress
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Address).NotNull();
                RuleFor(request => request.Address).SetValidator(new AddressValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public AddressDto Address { get; set; }
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
                var address = await _context.Addresses.SingleAsync(x => x.AddressId == request.Address.AddressId);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Address = address.ToDto()
                };
            }
            
        }
    }
}
