using AutoMapper;
using Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto;
using Innovectives.Groups.Business.Layer.Dtos.PaystackDtos;
using Paystack.Net.SDK.Models.Transfers.Recipient;

namespace Innovectives.Groups.Business.Layer.Mappers
{
    internal class CoreBankingMapper : Profile
    {
        public CoreBankingMapper()
        {
            CreateMap<ListBanksDto, BankListDto>(MemberList.Source)
                .ForMember(m => m.LongCode, m => m.MapFrom(r => r.LongCode))
                .ForMember(m => m.BankName, m => m.MapFrom(r => r.Name))
                .ForMember(m => m.Code, m => m.MapFrom(r => r.Code));

            CreateMap<VerifyAccntNumRespDto, AccountNumberVerifiedDto>(MemberList.Source)
               .ForMember(m => m.AccountName, m => m.MapFrom(r => r.account_name))
               .ForMember(m => m.AccountNumber, m => m.MapFrom(r => r.account_number))
               .ForMember(m => m.BankName, m => m.MapFrom(r => r.name))
               .ForMember(m => m.BankCode, m => m.MapFrom(r => r.code));

            CreateMap<BankTransfersReqDto, InitiateTransferDto>(MemberList.Source)
                .ForMember(m => m.amount, m => m.MapFrom(r => r.Amount))
                .ForMember(m => m.reason, m => m.MapFrom(r => r.Narration))
                .ForMember(m => m.reference, m => m.MapFrom(r => r.TransactionReference));

            CreateMap<BankTransferResp, BankTransfersReqDto>(MemberList.Source).ReverseMap();
        }
    }
}
