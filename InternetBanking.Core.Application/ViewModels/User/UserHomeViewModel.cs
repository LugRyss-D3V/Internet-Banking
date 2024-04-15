
namespace InternetBanking.Core.Application.ViewModels.User
{
    public class UserHomeViewModel
    {
        public List<UserViewModel>? Users { get; set; }
        public RegisterViewModel? Register { get; set; }

        public UserHomeViewModel(List<UserViewModel> users) 
        { 
            Users = users;
            Register = new RegisterViewModel();
        }
        public UserHomeViewModel(List<UserViewModel> users, RegisterViewModel vm)
        {
            Users = users;
            Register = vm;
        }

        public UserHomeViewModel()
        {
        }

    }
}
