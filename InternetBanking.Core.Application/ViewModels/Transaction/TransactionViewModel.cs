using InternetBanking.Core.Domain.Common;

namespace InternetBanking.Core.Application.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public string? FromId { get; set; }
        public int ToType { get; set; }

        public string? ToId { get; set; }
        public int FromType { get; set; }

        public double Cuantity { get; set; }
        public int Type { get; set; }
    }
}
