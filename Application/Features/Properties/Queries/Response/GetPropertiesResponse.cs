using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Properties.Queries.Response
{
   public  class GetPropertiesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }

        public DateTime CreatedOn { get; set; }
        public int IdOwner { get; set; }
    }
}
