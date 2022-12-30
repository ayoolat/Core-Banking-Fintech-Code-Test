using Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto;
using Innovectives.Groups.Business.Layer.Dtos.flutterwaveDtos;
using Innovectives.Groups.Business.Layer.Dtos.PaystackDtos;

namespace Innovectives.Groups.Business.Layer.PaymentServiceProviders.Interface
{
    public interface IPaymentProvider
    {
        public Task<List<ListBanksDto>> ListAllBanks();
        public Task<Dtos.PaystackDtos.VerifyAccntNumRespDto> VerifyAccountNumber(Dtos.CoreBankingDto.VerifyAccountNumberDto verifyAccountNumber);
        public Task<TransferRespDto> BankTransfer(BankTransfersReqDto bankTransfersReq);
        public Task<VerifyTransferResponse> VerifyTransaction(string transactionReference);
    }
}
