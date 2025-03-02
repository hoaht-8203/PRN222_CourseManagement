using Newtonsoft.Json;

namespace CourseManagement.Model.CustomResponses
{
    /// <summary>
    /// For the response with no data to return, use dynamic type or something similar (object,...). new ApiResponse<dynamic>()
    /// </summary>
    public sealed class ApiResponse<T>
    {
        public T? Data { get; set; }

        public int? StatusCode { get; set; }

        public bool IsSuccess { get => StatusCode >= 200 && StatusCode <= 299; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }


        public ApiResponse()
        {

        }

        public ApiResponse(int? statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public ApiResponse(int? statusCode, string? message, T? data)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
        }

        private static string GetDefaultMessageForStatusCode(int? statusCode)
        {
            return statusCode switch
            {
                404 => "Resource not found",
                500 => "An unhandled error occurred",
                _ => string.Empty,
            };
        }
    }

}
