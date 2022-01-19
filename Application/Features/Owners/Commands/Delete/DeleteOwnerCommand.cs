using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Owners.Commands.Delete
{
    public class DeleteOwnerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand, Result<int>>
        {

            private readonly IOwnerRepository _ownerRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteOwnerCommandHandler(IOwnerRepository ownerRepository, IUnitOfWork unitOfWork)
            {
                _ownerRepository = ownerRepository;
                _unitOfWork = unitOfWork;
            }
            public async Task<Result<int>> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
            {
                var owner = await _ownerRepository.GetByIdAsync(request.Id);
                await _ownerRepository.DeleteAsync(owner);
                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success(owner.Id);
            }
        }
    }
}

