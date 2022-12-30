using Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto;

namespace Innovectives.Groups.Business.Layer.Services.Intreface
{
    public interface ICoreBankingService
    {
        public Task<List<BankListDto>> ListBanks(string provider);
        public Task<AccountNumberVerifiedDto> ValidateAccountNumber(VerifyAccountNumberDto verifyAccountNumber);
        public Task<BankTransferResp> Transfer(BankTransfersReqDto bankTransfersReq);
        public Task VerifyTransaction(string transactionReference);

    }
}
