using InternetBanking.Core.Application.ViewModels.Products;

namespace InternetBanking.Core.Application.ViewModels.Transaction
{
    public class PaymentViewModel
    {
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public TransactionViewModel? Transaction { get; set; }
        public TransactionSaveViewModel? TransactionSave { get; set; }



    }
}
