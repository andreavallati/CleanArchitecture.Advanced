using CleanArchitecture.Advanced.Common.Application.DTOs.Base;

namespace CleanArchitecture.Advanced.Common.Application.DTOs
{
    public class EventLogDTO : DTOBase
    {
        public string? EventType { get; set; }
        public string? Description { get; set; }
    }
}
