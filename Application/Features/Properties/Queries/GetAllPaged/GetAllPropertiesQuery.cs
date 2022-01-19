using Application.Extensions;
using Application.Features.Properties.Queries.Response;
using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Properties.Queries.GetAllPaged
{
    public class GetAllPropertiesQuery : IRequest<PaginatedResult<GetPropertiesResponse>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public GetAllPropertiesQuery(int pageNumber,int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, PaginatedResult<GetPropertiesResponse>>
        {
            private readonly IPropertyRepository _repository;

            public GetAllPropertiesQueryHandler(IPropertyRepository repository)
            {
                _repository = repository;
            }

            public async Task<PaginatedResult<GetPropertiesResponse>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Property, GetPropertiesResponse>> expression = e => new GetPropertiesResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Address = e.Address,
                    CodeInternal = e.CodeInternal,
                    IdOwner = e.IdOwner,
                    Price = e.Price,
                    CreatedOn = e.CreatedOn,
                    Year = e.Year
                };

                var paginatedList = await _repository.Properties
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return paginatedList;

            }
        }
    }
}
