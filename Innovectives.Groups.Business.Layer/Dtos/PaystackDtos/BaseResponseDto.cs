namespace Innovectives.Groups.Business.Layer.Dtos.PaystackDtos
{
    public class BaseResponseDto<T>
    {
        public bool Status { get; set;}
        public string Message { get; set;}
        public T Data { get; set;}
    }
}
