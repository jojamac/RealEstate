using Application.Features.Owners.Commands.Create;
using Application.Features.Owners.Queries.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    internal class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<CreateOwnerCommand, Owner>().ReverseMap();
            CreateMap<GetOwnersResponse, Owner>().ReverseMap();
        }
    }
}
