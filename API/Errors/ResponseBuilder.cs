using System;

namespace API.Errors
{
    public class ResponseBuilder
    {
        public ResponseBuilder(int statusCode, string message = null)
        {
            Message = message ?? GetDefaultMessage(statusCode);
            StatusCode = statusCode;
        }

        public string Message { get; set; }

        public int StatusCode { get; set; }

        private string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request is made",
                401 => "You are not authorized",
                404 => "Resource not found",
                500 => "Internal server occured",
                _ => null
            };
        }

    }
}