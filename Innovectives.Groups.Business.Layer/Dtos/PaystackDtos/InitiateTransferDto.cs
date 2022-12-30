namespace Innovectives.Groups.Business.Layer.Dtos.PaystackDtos
{
    public class InitiateTransferDto
    {

        public string reason { get; set; }
        public string reference { get; set; }
        public string recipient { get; set; }
        public string amount { get; set; }
        public string source { get; set; }
        public string currency { get; set; }

    }
}
