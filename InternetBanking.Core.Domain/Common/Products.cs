
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Domain.Common
{
    public class Products : AuditableBaseEntity
    {
        [Key]
        public required string ProductId { get; set; }
        public required double Cuantity { get; set; }
        public required string UserId { get; set; }

    }
}
