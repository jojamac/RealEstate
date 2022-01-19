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

namespace Application.Features.PropertyTraces.Queries.GetById
{
   public class GetPropertyTraceByIdQuery : IRequest<Result<GetPropertyTraceResponse>>
    {
        public int Id { get; set; }

        public class GetPropertyImageByIdQueryHandler : IRequestHandler<GetPropertyTraceByIdQuery, Result<GetPropertyTraceResponse>>
        {
            private readonly IPropertyTraceRepository _propertyTraceRepository;
            private readonly IMapper _mapper;

            public GetPropertyImageByIdQueryHandler(IPropertyTraceRepository propertyTraceRepository, IMapper mapper)
            {
                _propertyTraceRepository = propertyTraceRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetPropertyTraceResponse>> Handle(GetPropertyTraceByIdQuery request, CancellationToken cancellationToken)
            {
                var propertyTrace = await _propertyTraceRepository.GetByIdAsync(request.Id);
                var mappedPropertyTrace = _mapper.Map<GetPropertyTraceResponse>(propertyTrace);
                return Result<GetPropertyTraceResponse>.Success(mappedPropertyTrace);
            }
        }
    }
}
