using TravelLine.Food.Core.Users;

namespace TravelLine.Food.Core.Transfers
{
    public interface ITransferService
    {
        void DeleteTransfer( int userId, int transferId );

        void SaveTransfer( UserModel user, TransferDto transferDto );

        TransferDto GetUserLegal( int userId, int transferRequestId );
    }
}
