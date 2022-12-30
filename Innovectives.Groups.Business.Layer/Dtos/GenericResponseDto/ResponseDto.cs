namespace Innovectives.Groups.Business.Layer.Dtos.GenericResponseDto
{
    public class ResponseDto<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Body { get; set; }
    }
}
