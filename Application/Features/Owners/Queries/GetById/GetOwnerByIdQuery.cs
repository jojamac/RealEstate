using Application.Features.Owners.Queries.Response;
using Application.Features.Properties.Queries.Response;
using Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Owners.Queries.GetById
{
   public  class GetOwnerByIdQuery : IRequest<Result<GetOwnersResponse>>
    {
        public int Id { get; set; }

        public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, Result<GetOwnersResponse>>
        {
            private readonly IOwnerCacheRepository _ownerCache;
            private readonly IMapper _mapper;

            public GetOwnerByIdQueryHandler(IOwnerCacheRepository ownerCache,IMapper mapper)
            {
                _ownerCache = ownerCache;
                _mapper = mapper;
            }
            public async Task<Result<GetOwnersResponse>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
            {
                var owner = await _ownerCache.GetByIdAsync(request.Id);
                var mappedOwner= _mapper.Map<GetOwnersResponse>(owner);
                return Result<GetOwnersResponse>.Success(mappedOwner);
            }
        }
    }
}
