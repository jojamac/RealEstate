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

namespace Application.Features.Owners.Commands.Create
{
    public partial class CreateOwnerCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public byte[] Photo { get; set; }

        public DateTime BirthDay { get; set; }

    }

    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, Result<int>>
    {

        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;


        private IUnitOfWork _unitOfWork { get; set; }

        public CreateOwnerCommandHandler(IOwnerRepository ownerRepository,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = _mapper.Map<Owner>(request);
            await _ownerRepository.InsertAsync(owner);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(owner.Id);
        }
    }


}
