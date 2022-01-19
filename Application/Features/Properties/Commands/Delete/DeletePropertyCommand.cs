using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Properties.Commands.Delete
{
    public class DeletePropertyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Result<int>>
        {

            private readonly IPropertyRepository _propertyRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeletePropertyCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
            {
                _propertyRepository = propertyRepository;
                _unitOfWork = unitOfWork;
            }
            public async Task<Result<int>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
            {
                var property = await _propertyRepository.GetByIdAsync(request.Id);
                await _propertyRepository.DeleteAsync(property);
                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success(property.Id);
            }
        }
    }
}
