using AspNetCoreHero.Abstractions.Domain;

namespace Domain.Entities
{
    public class PropertyImage: AuditableEntity
    {

        public byte[] File { get; set; }

        public bool Enabled { get; set; }

        public int IdProperty { get; set; }

        public virtual Property Property { get; set; }
    }
}
