using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PropertyImages.Queries.Response
{
    public class GetPropertyImageResponse
    {
        public int Id { get; set; }
        public byte[] File { get; set; }

        public bool Enabled { get; set; }

        public int IdProperty { get; set; }

    }
}
