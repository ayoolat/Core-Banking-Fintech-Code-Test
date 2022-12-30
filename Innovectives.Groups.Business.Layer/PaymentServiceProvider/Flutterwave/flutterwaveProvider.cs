using AutoMapper;
using Flutterwave.Net;
using Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto;
using Innovectives.Groups.Business.Layer.Dtos.flutterwaveDtos;
using Innovectives.Groups.Business.Layer.Dtos.PaystackDtos;
using Innovectives.Groups.Business.Layer.PaymentServiceProviders.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Innovectives.Groups.Business.Layer.PaymentServiceProvider.Flutterwave
{
    public class flutterwaveProvider : IPaymentProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private protected FlutterwaveApi flutterwaveApi { get; set; }
        static private string baseUrl { get; set; }
        protected static string apiKey { get; set; }
        public flutterwaveProvider(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            baseUrl = _configuration["ServiceProvidersBaseUrls:Flutterwave:Url"];
            apiKey = Environment.GetEnvironmentVariable("FLUTTERWAVE_API_SECRET_KEY");
            flutterwaveApi = new FlutterwaveApi(apiKey);
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
            var banks = flutterwaveApi.Banks.GetBanks(Country.Nigeria);
            return _mapper.Map<List<ListBanksDto>>(banks.Data);
        }

        public async Task<VerifyAccntNumRespDto> VerifyAccountNumber(VerifyAccountNumberDto verifyAccountNumber)
        {
            var verified = flutterwaveApi.Miscellaneous.VerifyBankAccount(verifyAccountNumber.AccountNumber, verifyAccountNumber.BankCode);
            if (verified.Status == "error")
                throw new Exception(verified.Message);
            var verifiedDto = new VerifyAccntNumRespDto()
            {
                account_number = verified.Data.AccountNumber,
                account_name = verified.Data.AccountName,
                code = verifyAccountNumber.BankCode
            };
            return verifiedDto;
        }

        public async Task<TransferRespDto> BankTransfer(BankTransfersReqDto bankTransfersReq)
        {
            var httpClient = GetHttpClient();
            TransferDto request = _mapper.Map<TransferDto>(bankTransfersReq);
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/transfer", data);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            TransferRespDto transferResponse = JsonConvert.DeserializeObject<TransferRespDto>(resp);
            if (transferResponse.status != "New")
                transferResponse.status = "Fail";

            transferResponse.status = "Success";
            return transferResponse;
        }


        Task<VerifyTransferResponse> IPaymentProvider.VerifyTransaction(string transactionReference)
        {
            throw new NotImplementedException();
        }
    }
}
