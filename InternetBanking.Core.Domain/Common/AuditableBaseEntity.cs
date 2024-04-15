
namespace InternetBanking.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
