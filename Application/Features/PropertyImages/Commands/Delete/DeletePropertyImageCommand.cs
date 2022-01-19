using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PropertyImages.Commands.Delete
{
    public class DeletePropertyImageCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeletePropertyImageCommandHandler : IRequestHandler<DeletePropertyImageCommand, Result<int>>
        {
            private readonly IPropertyImageRepository _propertyImageRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeletePropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository, IUnitOfWork unitOfWork)
            {
                _propertyImageRepository = propertyImageRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeletePropertyImageCommand request, CancellationToken cancellationToken)
            {
                var propertyImage = await _propertyImageRepository.GetByIdAsync(request.Id);
                await _propertyImageRepository.DeleteAsync(propertyImage);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(propertyImage.Id);
            }
        }
    }
}
