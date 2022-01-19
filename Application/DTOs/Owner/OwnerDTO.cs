using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Owner
{
   public  class OwnerDTO
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime BirthDay { get; set; }

        public IFormFile Image { get; set; }
    }
}
