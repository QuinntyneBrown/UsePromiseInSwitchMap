using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UsePromiseInSwitchMap.Api.Models;
using UsePromiseInSwitchMap.Api.Core;
using UsePromiseInSwitchMap.Api.Interfaces;

namespace UsePromiseInSwitchMap.Api.Features
{
    public class CreateCondoInfo
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CondoInfo).NotNull();
                RuleFor(request => request.CondoInfo).SetValidator(new CondoInfoValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public CondoInfoDto CondoInfo { get; set; }
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
                var condoInfo = new CondoInfo();
                
                _context.CondoInfos.Add(condoInfo);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    CondoInfo = condoInfo.ToDto()
                };
            }
            
        }
    }
}
