using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WiFiTracker.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace WiFiTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserStateService userState;
        public UserController(UserStateService _userState)
        {
            userState = _userState;
        }


        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery]string accountid, [FromQuery] string username, [FromQuery] string password)
        {
            if (await userState.Login(username, password, accountid))
            {
                //create a claim
                var claim = new Claim(ClaimTypes.NameIdentifier, userState.CurrentUser.Id.ToString());
                //create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
                //create claimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                //Sign in User
                await HttpContext.SignInAsync(claimsPrincipal);
            }
            var url = (HttpContext.Request.IsHttps == true ? "https://" : "http://") + HttpContext.Request.Host.Value + "/";
            return LocalRedirect("/");
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("/");
        }
    }
}
