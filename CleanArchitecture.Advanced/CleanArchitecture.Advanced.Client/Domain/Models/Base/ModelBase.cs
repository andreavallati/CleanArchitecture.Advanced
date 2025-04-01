using CleanArchitecture.Advanced.Common.Constants;

namespace CleanArchitecture.Advanced.Client.Domain.Models.Base
{
    public abstract class ModelBase
    {
        public long Id { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow.ToLocalTime();
        public string CreationUser { get; set; } = CommonConstants.CreationUsername;
        public DateTime? ModificationDate { get; set; }
        public string? ModificationUser { get; set; }
    }
}
