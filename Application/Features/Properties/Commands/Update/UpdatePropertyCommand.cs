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

namespace Application.Features.Propertys.Commands.Update
{
    public partial class UpdatePropertyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }
        public int IdOwner { get; set; }

    }

    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Result<int>>
    {

        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;


        public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository,IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {

            var property = await _propertyRepository.GetByIdAsync(request.Id);

            if (property == null)
            {
                return Result<int>.Fail($"Property Not Found");
            }
            else
            {
                property.Name = request.Name ?? property.Name;
                property.Price = (request.Price == 0) ? property.Price : request.Price;
                property.Address = request.Address ?? property.Address;
                property.CodeInternal = request.CodeInternal ?? property.CodeInternal;
                property.Year = request.Year ?? property.Year;
                property.IdOwner = (request.IdOwner == 0) ? property.IdOwner : request.IdOwner;

                await _propertyRepository.UpdateAsync(property);
                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success(property.Id);
            }
        }
    }


}
