namespace InternetBanking.Core.Application.Dtos.Account.Response
{
    public class UpdateResponse : BaseResponse
    {
        public string? Id { get; set; }
        public bool IsClient { get; set; }
    }
}
