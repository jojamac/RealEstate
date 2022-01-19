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

namespace Application.Features.PropertyImages.Commands.Create
{
    public class CreatePropertyImageCommand :IRequest<Result<int>>
    {
        public byte[] File { get; set; }

        public bool Enabled { get; set; }

        public int IdProperty { get; set; }
    }

    public class CreatePropertyImageCommandHandler : IRequestHandler<CreatePropertyImageCommand, Result<int>>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            var propertyImage = _mapper.Map<PropertyImage>(request);
            await _propertyImageRepository.InsertAsync(propertyImage);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(propertyImage.Id);
        }
    }
}
