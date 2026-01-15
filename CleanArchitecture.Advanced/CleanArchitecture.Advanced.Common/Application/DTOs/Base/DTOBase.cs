using CleanArchitecture.Advanced.Common.Constants;

namespace CleanArchitecture.Advanced.Common.Application.DTOs.Base
{
    public abstract class DTOBase
    {
        public long Id { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;
        public string CreationUser { get; set; } = CommonConstants.CreationUsername;
        public DateTime? ModificationDate { get; set; }
        public string? ModificationUser { get; set; }
    }
}
