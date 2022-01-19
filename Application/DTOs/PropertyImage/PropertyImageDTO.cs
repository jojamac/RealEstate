using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PropertyImage
{
    public class PropertyImageDTO
    {
        public bool Enabled { get; set; }

        public int IdProperty { get; set; }

        public IFormFile Image { get; set; }
    }
}
