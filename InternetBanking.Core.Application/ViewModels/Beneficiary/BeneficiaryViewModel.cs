

using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.ViewModels.Beneficiary
{
    public class BeneficiaryViewModel
    {
        public int UserId { get; set; }
        public int BeneficiaryId { get; set; }
        public UserViewModel? Beneficiary { get; set; }
        public int SavingAccountId { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public List<LoanViewModel>? Loans { get; set; }
        public List<CreditCardViewModel>? CreditCards { get; set; }
        public UserViewModel User { get; set; }
    }
}
