
using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class CreditCard : Products
    {
        public double? Limit { get; set; }

    }
}
