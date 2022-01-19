using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PropertyImages.Commands.Update
{
    public partial class UpdatePropertyImageCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public byte[] File { get; set; }

        public bool Enabled { get; set; }

        public int IdProperty { get; set; }
    }

    public class UpdatePropertyImageCommandHandler : IRequestHandler<UpdatePropertyImageCommand, Result<int>>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository,IUnitOfWork unitOfWork)
        {
            _propertyImageRepository = propertyImageRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(UpdatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            var propertyImage = await _propertyImageRepository.GetByIdAsync(request.Id);
            
            if(propertyImage  == null)
            {
                return Result<int>.Fail($"Property Image Not Found");
            }
            else
            {
                propertyImage.File = request.File ?? propertyImage.File;
                propertyImage.Enabled = request.Enabled;
                propertyImage.IdProperty = (request.IdProperty == 0) ? propertyImage.IdProperty : request.IdProperty;

                await _propertyImageRepository.UpdateAsync(propertyImage);
                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success(propertyImage.Id);
            }


        }
    }
}
