using Application.Features.PropertyImages.Queries.Response;
using Application.Interfaces.CacheRepositories;
using Application.Interfaces.Repositories;
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
   public class GetPropertyImageByIdPropertyQuery : IRequest<Result<List<GetPropertyImageResponse>>>
    {
        public int Id { get; set; }

        public class GetPropertyImageByIdPropertyQueryHandler : IRequestHandler<GetPropertyImageByIdPropertyQuery, Result<List<GetPropertyImageResponse>>>
        {
            private readonly IPropertyImageRepository _propertyImageRepository;
            private readonly IMapper _mapper;

            public GetPropertyImageByIdPropertyQueryHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
            {
                _propertyImageRepository = propertyImageRepository;
                _mapper = mapper;
            }

            public async Task<Result<List<GetPropertyImageResponse>>> Handle(GetPropertyImageByIdPropertyQuery request, CancellationToken cancellationToken)
            {
                var propertyImageList = await _propertyImageRepository.GetByIdPropertyAsync(request.Id);
                var mappedPropertyImages = _mapper.Map<List<GetPropertyImageResponse>>(propertyImageList);
                return Result<List<GetPropertyImageResponse>>.Success(mappedPropertyImages);
            }
        }
    }
}
