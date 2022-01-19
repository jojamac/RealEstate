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

namespace Application.Features.Properties.Queries.GetById
{
   public  class GetPropertyByIdQuery : IRequest<Result<GetPropertiesResponse>>
    {
        public int Id { get; set; }

        public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, Result<GetPropertiesResponse>>
        {
            private readonly IPropertyCacheRepository _propertyCache;
            private readonly IMapper _mapper;

            public GetPropertyByIdQueryHandler(IPropertyCacheRepository propertyCache,IMapper mapper)
            {
                _propertyCache = propertyCache;
                _mapper = mapper;
            }
            public async Task<Result<GetPropertiesResponse>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
            {
                var property = await _propertyCache.GetByIdAsync(request.Id);
                var mappedProperty = _mapper.Map<GetPropertiesResponse>(property);
                return Result<GetPropertiesResponse>.Success(mappedProperty);
            }
        }
    }
}
