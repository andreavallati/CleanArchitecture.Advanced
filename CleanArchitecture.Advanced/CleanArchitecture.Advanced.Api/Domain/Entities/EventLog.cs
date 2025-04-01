using CleanArchitecture.Advanced.Api.Domain.Entities.Base;

namespace CleanArchitecture.Advanced.Api.Domain.Entities
{
    public class EventLog : EntityBase
    {
        public string? EventType { get; set; }
        public string? Description { get; set; }
    }
}
