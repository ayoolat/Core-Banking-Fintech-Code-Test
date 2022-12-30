namespace Innovectives.Groups.Business.Layer.Dtos.PaystackDtos
{
    public class VerifyTransferResponse
    {
        public Recipient recipient { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string reference { get; set; }
        public string status { get; set; }
        public string reason { get; set; }
        public string transfer_code { get; set; }
        public string createdAt { get; set; }
    }
}
