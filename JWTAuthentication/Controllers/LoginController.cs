using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using JWTAuthentication.Model;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly SignInManager _signInManager;

        public LoginController(IConfiguration config, SignInManager signInManager)
        {
            _signInManager = signInManager;

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserModel loginUser)
        {
            var resp = await _signInManager.SignIn(loginUser.Email, loginUser.Password);
            if (resp.Success) 
                return Ok(resp.JWT);
            
            return Unauthorized();
        }
    }
}