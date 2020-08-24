using TravelLine.Food.Core.Transfers;

namespace TravelLine.Food.WebApi.Models.Users
{
    public class GetTransferResponse : Response
    {
        public TransferDto TransferDto { get; set; }
    }
}
