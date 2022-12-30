using System.ComponentModel.DataAnnotations;

namespace Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto
{
    public class BankTransfersReqDto
    {
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        [MaxLength(20)]
        public string Narration { get; set; }
        public string BeneficiaryAccountName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryBankCode { get; set; }
        public string TransactionReference { get; set; }
        public string? MaxRetryAttempt { get; set; }
        public string? CallBackUrl { get; set; }
        public string? Provider { get; set; }
    }
}
