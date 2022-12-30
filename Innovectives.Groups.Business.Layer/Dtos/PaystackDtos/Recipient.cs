namespace Innovectives.Groups.Business.Layer.Dtos.PaystackDtos
{
    public class Recipient
    {
        public string currency { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string recipient_code { get; set; }
        public string email { get; set; }
        public Details details { get; set; }

    }

    public class Details
    {
        public string account_number { get; set; }
        public string account_name { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
    }
}
