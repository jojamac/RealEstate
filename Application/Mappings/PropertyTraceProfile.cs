using Application.Features.PropertyImages.Queries.Response;
using Application.Features.PropertyTraces.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class PropertyTraceProfile:Profile
    {
        public PropertyTraceProfile()
        {
            CreateMap<CreatePropertyTraceCommand, PropertyTrace>().ReverseMap();
            CreateMap<GetPropertyTraceResponse, PropertyTrace>().ReverseMap();
        }
    }
}
