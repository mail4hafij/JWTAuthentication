using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        // GET api/status
        [HttpGet]
        [Authorize]
        public ActionResult<object> Status()
        {
            var loggedInUser = HttpContext.User;
            
            if (loggedInUser.HasClaim(c => c.Type == ClaimTypes.Role) && 
                loggedInUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "CLIENT")
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: Invalid user");
        }
    }
}
