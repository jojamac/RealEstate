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

namespace Application.Features.Propertys.Commands.Create
{
    public partial class CreatePropertyCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }
        public int IdOwner { get; set; }

    }

    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Result<int>>
    {

        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;


        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Property>(request);
            await _propertyRepository.InsertAsync(property);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(property.Id);
        }
    }


}
