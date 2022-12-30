namespace Innovectives.Groups.Business.Layer.Dtos.CoreBankingDto
{
    public class VerifyAccountNumberDto
    {
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public string? Provider { get; set; }
    }
}
