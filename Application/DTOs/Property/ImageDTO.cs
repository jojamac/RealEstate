using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Property
{
    public class ImageDTO
    {
        public string FileName { get; set; }
        public IFormFile Image { get; set; }
    }
}
