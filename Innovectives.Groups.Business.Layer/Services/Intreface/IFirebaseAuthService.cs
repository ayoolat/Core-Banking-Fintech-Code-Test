using Firebase.Auth;
using Innovectives.Groups.Business.Layer.Dtos.FirebaseAuth;

namespace Innovectives.Groups.Business.Layer.Services.Intreface
{
    public interface IFirebaseAuthService
    {
        public Task<FirebaseAuthLink> Register(Register registerationDto);
        public Task<FirebaseAuthLink> Login(Login loginDto);
    }
}
