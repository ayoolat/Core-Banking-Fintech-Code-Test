using AutoMapper;
using Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto;
using Innovectives.Groups.Business.Layer.PaymentServiceProviders;
using Innovectives.Groups.Business.Layer.PaymentServiceProviders.Interface;
using Innovectives.Groups.Business.Layer.Services.Intreface;
using Microsoft.Extensions.Configuration;

namespace Innovectives.Groups.Business.Layer.Services
{
    public class CoreBankingService : ICoreBankingService
    {
        private protected ConcretePaymentServiceProvider _paymentServiceProvider;
        private readonly IConfiguration _configuration;
        private protected IPaymentProvider _factory;
        private readonly IMapper _mapper;
        private string _provider;

        public CoreBankingService(IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _mapper = mapper;
            _provider = _configuration["ServiceProvidersBaseUrls:Paystack:Name"];
        }

        private async Task GetProvider(string provider)
        {
            _paymentServiceProvider = new ConcretePaymentServiceProvider(_configuration, _mapper);
            _factory = _paymentServiceProvider.FactoryMethod(provider);
        }

        public async Task<List<BankListDto>> ListBanks(string provider)
        {
            await GetProvider(provider);
            var bankList = await _factory.ListAllBanks();
            return _mapper.Map<List<BankListDto>>(bankList);
        }

        public async Task<AccountNumberVerifiedDto> ValidateAccountNumber(VerifyAccountNumberDto verifyAccountNumber)
        {
            await GetProvider(verifyAccountNumber.Provider);
            var validated = await _factory.VerifyAccountNumber(verifyAccountNumber);
            return _mapper.Map<AccountNumberVerifiedDto>(validated);
        }

        public async Task<BankTransferResp> Transfer(BankTransfersReqDto bankTransfersReq)
        {
            await GetProvider(bankTransfersReq.Provider);
            Guid myuuid = Guid.NewGuid();
            bankTransfersReq.TransactionReference = $"bkc-{myuuid}";
            var transferResponse = await _factory.BankTransfer(bankTransfersReq);
            var transferResponseDto = _mapper.Map<BankTransferResp>(bankTransfersReq);
            transferResponseDto.ResponseMessage = transferResponse.status == "Success" ? "Transfer_Successful" : "Transfer_Not_Successful";
            transferResponseDto.TransactionDateTime = transferResponse.created_at;
            transferResponseDto.ResponseCode = transferResponse.status == "Success"? "00" : "99";
            transferResponseDto.Status = transferResponse.status == "Success" ? "SUCCESS" : "FAILURE";
            return transferResponseDto;
        }

        public async Task VerifyTransaction(string transactionReference)
        {
            await _factory.VerifyTransaction(transactionReference);
        }
    }
}
