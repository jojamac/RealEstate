using AspNetCoreHero.Abstractions.Domain;
using System;

namespace Domain.Entities
{
    public class PropertyTrace : AuditableEntity
    {
        public DateTime DateSale { get; set; }

        public string Name { get; set; }

       public decimal Value { get; set; }

        public string Tax { get; set; }

        public int IdProperty { get; set; }

        public virtual Property Property { get; set; }
    }
}
