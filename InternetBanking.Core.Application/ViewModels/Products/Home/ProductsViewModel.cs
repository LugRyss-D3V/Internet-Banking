using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.ViewModels.Products.Home
{
    public class ProductsViewModel
    {
        public UserViewModel? User { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public List<LoanViewModel>? Loans { get; set; }
        public List<CreditCardViewModel>? CreditCards { get; set; }

        public SavingAccountViewModel? SavingAccount { get; set; }
        public LoanViewModel? Loan { get; set; }
        public CreditCardViewModel? CreditCard { get; set; }
    }
}
