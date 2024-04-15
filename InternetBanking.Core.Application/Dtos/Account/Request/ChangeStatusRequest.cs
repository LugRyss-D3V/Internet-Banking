
namespace InternetBanking.Core.Application.Dtos.Account.Request
{
    public class ChangeStatusRequest
    {
        public string? UserId { get; set; }

        public ChangeStatusRequest()
        {

        }

        public ChangeStatusRequest(string userId)
        {
            UserId = userId;
        }
    }
}
