
using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Domain.Entities
{
    public class SavingAccount : Products
    {
        public int? SavingAccountType { get; set; }
    }
}
