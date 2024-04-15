namespace InternetBanking.Core.Application.Dtos
{
    public class BaseResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }

        public BaseResponse()
        {
            HasError = false;
        }
    }
}
