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

namespace Application.Features.PropertyImages.Queries.GetById
{
   public class GetPropertyImageByIdQuery :IRequest<Result<GetPropertyImageResponse>>
    {
        public int Id { get; set; }

        public class GetPropertyImageByIdQueryHandler : IRequestHandler<GetPropertyImageByIdQuery, Result<GetPropertyImageResponse>>
        {
            private readonly IPropertyImageCacheRepository _propertyImageCacheRepository;
            private readonly IMapper _mapper;

            public GetPropertyImageByIdQueryHandler(IPropertyImageCacheRepository propertyImageCacheRepository, IMapper mapper)
            {
                _propertyImageCacheRepository = propertyImageCacheRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetPropertyImageResponse>> Handle(GetPropertyImageByIdQuery request, CancellationToken cancellationToken)
            {
                var propertyImage = await _propertyImageCacheRepository.GetByIdAsync(request.Id);
                var mappedPropertyImage = _mapper.Map<GetPropertyImageResponse>(propertyImage);
                return Result<GetPropertyImageResponse>.Success(mappedPropertyImage);
            }
        }
    }
}
