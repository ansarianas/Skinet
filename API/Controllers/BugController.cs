using Microsoft.AspNetCore.Mvc;
using API.Errors;
using Infrastructure.Data;

namespace API.Controllers
{
    public class BugController : BaseApiController
    {
        private readonly StoreContext _context;

        public BugController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult NotFoundError()
        {
            var notFound = _context.Products.Find(223);
            if (notFound == null)
            {
                return NotFound(new ResponseBuilder(404));
            }
            return Ok();
        }

        [HttpGet("server")]
        public ActionResult ServerError()
        {
            var notFound = _context.Products.Find(223);
            var err = notFound.ToString();
            return Ok();
        }

        [HttpGet("bad")]
        public ActionResult BadError()
        {
            return BadRequest(new ResponseBuilder(400));
        }

        [HttpGet("math")]
        public ActionResult MathError()
        {
            var zero = 0;
            var a = 15 / zero;
            return Ok();
        }
    }
}