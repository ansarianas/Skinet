using Microsoft.AspNetCore.Mvc;
using API.Errors;

namespace API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ResponseBuilder(code));
        }
    }
}