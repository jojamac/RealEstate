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

namespace Application.Features.Properties.Queries.GetAllCached
{
    public class GetAllPropertiesCachedQuery : IRequest<Result<List<GetPropertiesResponse>>>
    {
        public GetAllPropertiesCachedQuery()
        {
        }

        public class GetAllPropertiesCachedQueryHandler : IRequestHandler<GetAllPropertiesCachedQuery, Result<List<GetPropertiesResponse>>>
        {
            private readonly IPropertyCacheRepository _propertyCache;
            private readonly IMapper _mapper;

            public GetAllPropertiesCachedQueryHandler(IPropertyCacheRepository propertyCache, IMapper mapper)
            {
                _propertyCache = propertyCache;
                _mapper = mapper;
            }

            public async Task<Result<List<GetPropertiesResponse>>> Handle(GetAllPropertiesCachedQuery request, CancellationToken cancellationToken)
            {
                var propertyList = await _propertyCache.GetCachedListAsync();
                var mappedProperties = _mapper.Map<List<GetPropertiesResponse>>(propertyList);
                return Result<List<GetPropertiesResponse>>.Success(mappedProperties);
            }
        }
    }
}
