using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PropertyTraces.Commands.Update
{
    public partial class UpdatePropertyTraceCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DateTime DateSale { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Tax { get; set; }

        public int IdProperty { get; set; }
    }

    public class UpdatePropertyTraceCommandHandler : IRequestHandler<UpdatePropertyTraceCommand, Result<int>>
    {
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyTraceCommandHandler(IPropertyTraceRepository propertyTraceRepository, IUnitOfWork unitOfWork)
        {
            _propertyTraceRepository = propertyTraceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(UpdatePropertyTraceCommand request, CancellationToken cancellationToken)
        {
            var propertyTrace = await _propertyTraceRepository.GetByIdPropertyTrace(request.Id);

            if (propertyTrace == null)
            {
                return Result<int>.Fail($"Property Trace Not Found");
            }
            else
            {
                propertyTrace.Name = request.Name ?? propertyTrace.Name;
                propertyTrace.Value = (request.Value == 0) ? propertyTrace.Value : request.Value;
                propertyTrace.Tax = request.Tax ?? propertyTrace.Tax;
                propertyTrace.DateSale = request.DateSale;
                propertyTrace.IdProperty = (request.IdProperty == 0) ? propertyTrace.IdProperty : request.IdProperty;

                await _propertyTraceRepository.UpdateAsync(propertyTrace);
                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success(propertyTrace.Id);
            }


        }
    }
}
