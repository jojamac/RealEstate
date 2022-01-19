using Application.Features.PropertyImages.Queries.Response;
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

namespace Application.Features.PropertyImages.Queries.GetAllCached
{
    public class GetAllPropertyImageCacheQuery:IRequest<Result<List<GetPropertyImageResponse>>>
    {
        public GetAllPropertyImageCacheQuery()
        {
        }

        public class GetAllPropertyImageCacheQueryHandler : IRequestHandler<GetAllPropertyImageCacheQuery, Result<List<GetPropertyImageResponse>>>
        {
            private readonly IPropertyImageCacheRepository _propertyImageCacheRepository;
            private readonly IMapper _mapper;

            public GetAllPropertyImageCacheQueryHandler(IPropertyImageCacheRepository propertyImageCacheRepository ,IMapper mapper)
            {
                _propertyImageCacheRepository = propertyImageCacheRepository;
                _mapper = mapper;
            }

            public async Task<Result<List<GetPropertyImageResponse>>> Handle(GetAllPropertyImageCacheQuery request, CancellationToken cancellationToken)
            {
                var propertyImageList = await _propertyImageCacheRepository.GetCachedListAsync();
                var mappedPropertyImages = _mapper.Map<List<GetPropertyImageResponse>>(propertyImageList);
                return Result<List<GetPropertyImageResponse>>.Success(mappedPropertyImages);
            }
        }
    }
}
