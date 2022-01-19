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

namespace Application.Features.Owners.Queries.GetAllCached
{
    public class GetAllOwnerCachedQuery:IRequest<Result<List<GetOwnersResponse>>>
    {
        public GetAllOwnerCachedQuery()
        {
        }

        public class GetAllOwnerCachedQueryHandler : IRequestHandler<GetAllOwnerCachedQuery, Result<List<GetOwnersResponse>>>
        {
            private readonly IOwnerCacheRepository _ownerCache;
            private readonly IMapper _mapper;

            public GetAllOwnerCachedQueryHandler(IOwnerCacheRepository ownerCache, IMapper mapper)
            {
                _ownerCache = ownerCache;
                _mapper = mapper;
            }

            public async Task<Result<List<GetOwnersResponse>>> Handle(GetAllOwnerCachedQuery request, CancellationToken cancellationToken)
            {
                var ownerList = await _ownerCache.GetCachedListAsync();
                var mappedOwners = _mapper.Map<List<GetOwnersResponse>>(ownerList);
                return Result<List<GetOwnersResponse>>.Success(mappedOwners);
            }
        }
    }
}
