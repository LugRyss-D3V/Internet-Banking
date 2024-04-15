
using InternetBanking.Core.Application.Enums;

namespace InternetBanking.Core.Application.Dtos.Product.Request
{
    public class DebitRequest
    {
        public string? ProductId { get; set; }
        public double Amount { get; set; }
        public ProductType ProductType { get; set; }
    }
}
