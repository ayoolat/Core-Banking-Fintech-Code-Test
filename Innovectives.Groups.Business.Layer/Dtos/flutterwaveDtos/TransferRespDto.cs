namespace Innovectives.Groups.Business.Layer.Dtos.flutterwaveDtos
{
    public class TransferRespDto
    {
        public string reference { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
        public string account_number { get; set; }
        public string status { get; set; }
        public string Transfer_code { get; set; }
        public int id { get; set; }
        public DateTime created_at { get; set; }
    }
}
