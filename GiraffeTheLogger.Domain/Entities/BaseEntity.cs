using System;
using GiraffeTheLogger.Domain.Extensions;

namespace GiraffeTheLogger.Domain.Entities
{
    public class BaseEntity{
        public Guid Id { get; set; } = Guid.NewGuid();
        public double ReceiveDateTime { get; set; }
    }
}