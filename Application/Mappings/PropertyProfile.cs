using Application.Features.Properties.Queries.Response;
using Application.Features.Propertys.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    internal class PropertyProfile: Profile
    {
        public PropertyProfile()
        {
            CreateMap<CreatePropertyCommand, Property>().ReverseMap();
            CreateMap<GetPropertiesResponse, Property>().ReverseMap();
        }
    }
}
