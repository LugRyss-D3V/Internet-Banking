
namespace InternetBanking.Core.Application.ViewModels.Home
{
    public class HomeViewModel
    {
        public int TransactionsToday { get; set; }
        public int TransactionsTotal { get; set; }
        public int PaymentsToday {  get; set; }
        public int PaymentsTotal { get; set; }
        public int AssignedProducts { get; set; }
        public int ActiveClients {  get; set; }
        public int DeactiveClients {  get; set; }
    }
}
