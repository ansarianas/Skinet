namespace API.Errors
{
    public class ResponseBuilderException : ResponseBuilder
    {
        public ResponseBuilderException(int statusCode, int exceptionLine = 0, string message = null,
        string fileName = null, string filePath = null, string methodTrace = null) : base(statusCode, message)
        {
            ExceptionLine = exceptionLine;
            FileName = fileName;
            FilePath = filePath;
            MethodTrace = methodTrace;
        }

        public int ExceptionLine { get; set; }

        public string FilePath { get; set; }

        public string FileName { get; set; }

        public string MethodTrace { get; set; }
    }
}