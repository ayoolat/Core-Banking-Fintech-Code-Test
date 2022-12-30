using Innovectives.Groups.Business.Layer.Dtos.GenericResponseDto;

namespace Innovectives.Groups.Business.Layer.Utils
{
    public class ResponseService<T>
    {
        public static ResponseDto<T> PrintResponse(T body, string message, int code)
        {
            var response = new ResponseDto<T>();

            response.Body = body;
            response.Message = message;
            response.Code = code;

            return response;
        }
    }
}
