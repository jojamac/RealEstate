using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PropertyTraces.Commands.Delete
{
    public class DeletePropertyTraceCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeletePropertyTraceCommandHandler : IRequestHandler<DeletePropertyTraceCommand, Result<int>>
        {
            private readonly IPropertyTraceRepository _propertyTraceRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeletePropertyTraceCommandHandler(IPropertyTraceRepository propertyTraceRepository, IUnitOfWork unitOfWork)
            {
                _propertyTraceRepository = propertyTraceRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeletePropertyTraceCommand request, CancellationToken cancellationToken)
            {
                var propertyTrace = await _propertyTraceRepository.GetByIdPropertyTrace(request.Id);
                await _propertyTraceRepository.DeleteAsync(propertyTrace);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(propertyTrace.Id);
            }
        }
    }
}
