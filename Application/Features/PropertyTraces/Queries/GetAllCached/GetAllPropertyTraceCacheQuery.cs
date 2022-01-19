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

namespace Application.Features.PropertyTraces.Queries.GetAllCached
{
    public class GetAllPropertyTraceCacheQuery : IRequest<Result<List<GetPropertyTraceResponse>>>
    {
        public GetAllPropertyTraceCacheQuery()
        {
        }

        public class GetAllPropertyTraceCacheQueryHandler : IRequestHandler<GetAllPropertyTraceCacheQuery, Result<List<GetPropertyTraceResponse>>>
        {
            private readonly IPropertyTraceCacheRepository _propertyTraceCacheRepository;
            private readonly IMapper _mapper;

            public GetAllPropertyTraceCacheQueryHandler(IPropertyTraceCacheRepository propertyTraceCacheRepository ,IMapper mapper)
            {
                _propertyTraceCacheRepository = propertyTraceCacheRepository;
                _mapper = mapper;
            }

            public async Task<Result<List<GetPropertyTraceResponse>>> Handle(GetAllPropertyTraceCacheQuery request, CancellationToken cancellationToken)
            {
                var propertyTraceList = await _propertyTraceCacheRepository.GetCachedListAsync();
                var mappedPropertyTrace = _mapper.Map<List<GetPropertyTraceResponse>>(propertyTraceList);
                return Result<List<GetPropertyTraceResponse>>.Success(mappedPropertyTrace);
            }
        }
    }
}
