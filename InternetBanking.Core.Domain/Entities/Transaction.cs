
using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class Transaction : AuditableBaseEntity
    {
        public int TransactionId { get; set; }
        public string? FromId { get; set; }
        public string? ToId { get; set; }
        public double Cuantity { get; set; }
        public int Type { get; set; }
        public int FromType { get; set; }
        public int ToType { get; set; }
    }
}
