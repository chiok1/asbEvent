
namespace asbEvent.DTOs
{
    public class ServiceResult
    {
        public required string StatusCode { get; set; }
        public required string StatusDescription { get; set; }

        public static ServiceResult SuccessResult(string statusCode = "0", string description = "")
        {
            return new ServiceResult
            {
                StatusCode = statusCode,
                StatusDescription = description
            };
        }

        public static ServiceResult ErrorResult(string statusCode, string description)
        {
            return new ServiceResult
            {
                StatusCode = statusCode,
                StatusDescription = description
            };
        }
    }
}