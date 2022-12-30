using AutoMapper;
using Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto;
using Innovectives.Groups.Business.Layer.Dtos.flutterwaveDtos;
using Innovectives.Groups.Business.Layer.Dtos.PaystackDtos;
using Innovectives.Groups.Business.Layer.PaymentServiceProviders.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Paystack.Net.SDK;
using System.Net.Http.Headers;
using System.Text;

namespace Innovectives.Groups.Business.Layer.PaymentServiceProviders.Paystack
{
    public class PaystackProvider : IPaymentProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private protected PayStackApi paystackApi { get; set; }
        static private string baseUrl { get; set; }
        protected static string apiKey { get; set; }

        public PaystackProvider(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            baseUrl = _configuration["ServiceProvidersBaseUrls:Paystack:Url"];
            apiKey = Environment.GetEnvironmentVariable("PAYSTACK_API_SECRET_KEY");
            paystackApi = new PayStackApi(apiKey);
        }

        private static HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            return client;
        }
       public async Task<List<ListBanksDto>> ListAllBanks()
       {
            try
            {
                var httpClient = GetHttpClient();
                var response = await httpClient.GetAsync("/bank");
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();
                BaseResponseDto<List<ListBanksDto>> banks = JsonConvert.DeserializeObject<BaseResponseDto<List<ListBanksDto>>>(resp);
                return banks.Data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
       }

        public async Task<VerifyAccntNumRespDto> VerifyAccountNumber(VerifyAccountNumberDto verifyAccountNumber)
        {
            var httpClient = GetHttpClient();
            var url = baseUrl + "/bank/resolve?";
            var builder = new UriBuilder(url);
            builder.Query = $"account_number={verifyAccountNumber.AccountNumber}&bank_code={verifyAccountNumber.BankCode}";
            url = builder.ToString();   
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            BaseResponseDto<VerifyAccntNumRespDto> bankAccount = JsonConvert.DeserializeObject<BaseResponseDto<VerifyAccntNumRespDto>>(resp);
            bankAccount.Data.code = verifyAccountNumber.BankCode;
            return bankAccount.Data;
        }

        public async Task<TransferRespDto> BankTransfer(BankTransfersReqDto bankTransfersReq)
        {
            var recipient = await paystackApi.Transfers.CreateTransferRecipient(
                name: bankTransfersReq.BeneficiaryAccountName, account_number: bankTransfersReq.BeneficiaryAccountNumber, 
                bank_code: bankTransfersReq.BeneficiaryBankCode, description: bankTransfersReq.Narration, type: "nuban",
                currency: bankTransfersReq.CurrencyCode
            );
            if (!recipient.status)
                throw new Exception(recipient.message);

            var httpClient = GetHttpClient();
            InitiateTransferDto request = _mapper.Map<InitiateTransferDto>(bankTransfersReq);
            request.source = "balance";
            request.recipient = recipient.data.recipient_code;
            request.currency = "NGN";
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/transfer", data);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            BaseResponseDto<InitiateTranferRespDto> initTransfer = JsonConvert.DeserializeObject<BaseResponseDto<InitiateTranferRespDto>>(resp);
            return _mapper.Map<TransferRespDto>(initTransfer);
        }

       
        public async Task<VerifyTransferResponse> VerifyTransaction(string transactionReference)
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync($"/transfer/verify/{transactionReference}");
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            BaseResponseDto<VerifyTransferResponse> verifiedResponse = JsonConvert.DeserializeObject<BaseResponseDto<VerifyTransferResponse>>(resp);
            return verifiedResponse.Data;
        }

    }
}
