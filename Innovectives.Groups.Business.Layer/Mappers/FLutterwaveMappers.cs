using AutoMapper;
using Flutterwave.Net;
using Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto;
using Innovectives.Groups.Business.Layer.Dtos.flutterwaveDtos;
using Innovectives.Groups.Business.Layer.Dtos.PaystackDtos;

namespace Innovectives.Groups.Business.Layer.Mappers
{
    public class FLutterwaveMappers : Profile
    {
        public FLutterwaveMappers()
        {
            CreateMap<ListBanksDto, Bank>(MemberList.Source).ReverseMap();

            CreateMap<VerifyAccntNumRespDto, BankAccount>()
                .ForMember(m => m.AccountName, m => m.MapFrom(r => r.account_name))
                .ForMember(m => m.AccountNumber, m => m.MapFrom(r => r.account_number));

            CreateMap<BankAccount, VerifyAccntNumRespDto>()
                .ForMember(m => m.account_name, m => m.MapFrom(r => r.AccountName))
                .ForMember(m => m.account_number, m => m.MapFrom(r => r.AccountNumber));

            CreateMap<BankTransfersReqDto, TransferDto>()
                .ForMember(m => m.amount, m => m.MapFrom(r => r.Amount))
                .ForMember(m => m.account_bank, m => m.MapFrom(r => r.BeneficiaryBankCode))
                .ForMember(m => m.account_number, m => m.MapFrom(r => r.BeneficiaryAccountNumber))
                .ForMember(m => m.narration, m => m.MapFrom(r => r.Narration))
                .ForMember(m => m.currency, m => m.MapFrom(r => r.CurrencyCode))
                .ForMember(m => m.callback_url, m => m.MapFrom(r => r.CallBackUrl))
                .ForMember(m => m.debit_currency, m => m.MapFrom(r => r.CurrencyCode))
                .ForMember(m => m.reference, m => m.MapFrom(r => r.TransactionReference));

            CreateMap<TransferDto, BankTransfersReqDto>()
                .ForMember(m => m.Amount, m => m.MapFrom(r => r.amount))
                .ForMember(m => m.BeneficiaryBankCode, m => m.MapFrom(r => r.account_bank))
                .ForMember(m => m.BeneficiaryAccountNumber, m => m.MapFrom(r => r.account_number))
                .ForMember(m => m.Narration, m => m.MapFrom(r => r.narration))
                .ForMember(m => m.CurrencyCode, m => m.MapFrom(r => r.currency))
                .ForMember(m => m.CallBackUrl, m => m.MapFrom(r => r.callback_url))
                .ForMember(m => m.CurrencyCode, m => m.MapFrom(r => r.debit_currency))
                .ForMember(m => m.TransactionReference, m => m.MapFrom(r => r.reference));



        }
    }
}
