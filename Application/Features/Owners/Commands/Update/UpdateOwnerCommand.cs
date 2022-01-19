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

namespace Application.Features.Owners.Commands.Update
{
    public partial class UpdateOwnerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public byte[] Photo { get; set; }

        public DateTime BirthDay { get; set; }

    }

    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, Result<int>>
    {

        private readonly IOwnerRepository _ownerRepository;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateOwnerCommandHandler(IOwnerRepository ownerRepository,IUnitOfWork unitOfWork)
        {
            _ownerRepository = ownerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {

            var owner = await _ownerRepository.GetByIdAsync(request.Id);

            if (owner == null)
            {
                return Result<int>.Fail($"Owner Not Found");
            }
            else
            {
                owner.Name = request.Name ?? owner.Name;
                owner.Address = request.Address ?? owner.Address;
                owner.Photo = request.Photo ?? owner.Photo;
                owner.BirthDay = request.BirthDay ;

                await _ownerRepository.UpdateAsync(owner);
                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success(owner.Id);
            }
        }
    }


}
