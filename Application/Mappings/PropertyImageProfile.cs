using Application.Features.PropertyImages.Commands.Create;
using Application.Features.PropertyImages.Queries.Response;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class PropertyImageProfile:Profile
    {
        public PropertyImageProfile()
        {
            CreateMap<CreatePropertyImageCommand, PropertyImage>().ReverseMap();
            CreateMap<GetPropertyImageResponse, PropertyImage>().ReverseMap();
        }
    }
}
