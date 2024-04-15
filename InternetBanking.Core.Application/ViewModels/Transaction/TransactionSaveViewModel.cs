
namespace InternetBanking.Core.Application.ViewModels.Transaction
{
    public class TransactionSaveViewModel
    {
        public string? FromId { get; set; }
        public int? FromType { get; set; }
        public string? ToId { get; set; }
        public int ToType { get; set; }
        public double Cuantity { get; set; }
        public int Type { get; set; }
    }
}
