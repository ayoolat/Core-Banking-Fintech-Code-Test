using Firebase.Auth;
using Innovectives.Groups.Business.Layer.Dtos.FirebaseAuth;
using Innovectives.Groups.Business.Layer.Services.Intreface;
using Microsoft.AspNetCore.Mvc;

namespace Innovectives.Group.Application.Layer.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        protected readonly IFirebaseAuthService _firebaseAuthService;
       public AuthController(IFirebaseAuthService firebaseAuthService)
        {
            _firebaseAuthService = firebaseAuthService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<FirebaseAuthLink>> Register(Register register)
        {
            return Ok(await _firebaseAuthService.Register(register));
        }

        [HttpPost("login")]
        public async Task<ActionResult<FirebaseAuthLink>> Login(Login login)
        {
            return Ok(await _firebaseAuthService.Login(login));
        }

    }
}
