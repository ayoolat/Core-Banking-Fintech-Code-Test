namespace Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto
{
    public class BankTransferResp
    {
        public int Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string BeneficiaryAccountName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryBankCode { get; set; }
        public string TransactionReference { get; set; }
        public string ResponseCode { get; set; }
        public string SessionId { get; set; }
        public string ResponseMessage { get; set; }
        public string Status { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}
