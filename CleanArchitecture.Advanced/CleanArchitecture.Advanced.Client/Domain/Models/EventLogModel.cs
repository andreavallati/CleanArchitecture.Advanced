using CleanArchitecture.Advanced.Client.Domain.Models.Base;

namespace CleanArchitecture.Advanced.Client.Domain.Models
{
    public class EventLogModel : ModelBase
    {
        public string? EventType { get; set; }
        public string? Description { get; set; }
    }
}
