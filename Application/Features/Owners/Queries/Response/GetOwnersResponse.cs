using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Owners.Queries.Response
{
   public  class GetOwnersResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public byte[] Photo { get; set; }

        public DateTime BirthDay { get; set; }
    }
}
