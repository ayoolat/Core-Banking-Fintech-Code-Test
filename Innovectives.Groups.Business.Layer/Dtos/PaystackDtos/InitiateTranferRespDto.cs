namespace Innovectives.Groups.Business.Layer.Dtos.PaystackDtos
{
    public class InitiateTranferRespDto
    {
        public string Reference { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Balance { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string Transfer_code { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
