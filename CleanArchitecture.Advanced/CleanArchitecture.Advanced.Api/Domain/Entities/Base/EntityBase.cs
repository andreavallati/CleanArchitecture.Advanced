using CleanArchitecture.Advanced.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Advanced.Api.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        [Key]
        public long Id { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow.ToLocalTime();
        public string CreationUser { get; set; } = CommonConstants.CreationUsername;
        public DateTime? ModificationDate { get; set; }
        public string? ModificationUser { get; set; }
    }
}
