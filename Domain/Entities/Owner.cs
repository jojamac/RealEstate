using AspNetCoreHero.Abstractions.Domain;
using System;

namespace Domain.Entities
{
    public class Owner: AuditableEntity
    {
       

        public string Name { get; set; }

        public string Address { get; set; }

        public byte[] Photo { get; set; }

        public DateTime BirthDay { get; set; }
    }
}
