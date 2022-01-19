using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PropertyImages.Queries.Response
{
    public class GetPropertyTraceResponse
    {
        public int Id { get; set; }
        public DateTime DateSale { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Tax { get; set; }

        public int IdProperty { get; set; }


    }
}
