using System;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Image : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
