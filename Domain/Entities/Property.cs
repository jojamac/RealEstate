using AspNetCoreHero.Abstractions.Domain;

namespace Domain.Entities
{
    public class Property: AuditableEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public string CodeInternal { get; set; }

        public string Year { get; set; }

        public int IdOwner { get; set; }

        public virtual Owner Owner { get; set; }


    }
}
