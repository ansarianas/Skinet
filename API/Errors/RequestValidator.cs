using System.Collections.Generic;

namespace API.Errors
{
    public class RequestValidator : ResponseBuilder
    {
        public RequestValidator() : base(400) { }

        public IEnumerable<string> Errors { get; set; }
    }
}
