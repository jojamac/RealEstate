using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PropertyTraces.Commands.Create
{
    public class CreatePropertyTraceCommand : IRequest<Result<int>>
    {
        public DateTime DateSale { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Tax { get; set; }

        public int IdProperty { get; set; }
    }

    public class CreatePropertyTraceCommandHandler : IRequestHandler<CreatePropertyTraceCommand, Result<int>>
    {
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePropertyTraceCommandHandler(IPropertyTraceRepository propertyTraceRepository,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _propertyTraceRepository = propertyTraceRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreatePropertyTraceCommand request, CancellationToken cancellationToken)
        {
            var propertyTrace = _mapper.Map<PropertyTrace>(request);
            await _propertyTraceRepository.InsertAsync(propertyTrace);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(propertyTrace.Id);
        }
    }
}
