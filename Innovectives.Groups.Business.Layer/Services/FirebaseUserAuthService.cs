using AutoMapper;
using Firebase.Auth;
using Innovectives.Groups.Business.Layer.Dtos.FirebaseAuth;
using Innovectives.Groups.Business.Layer.Services.Intreface;
using Microsoft.Extensions.Configuration;

namespace Innovectives.Groups.Business.Layer.Services
{
    public class FirebaseUserAuthService : IFirebaseAuthService
    {
        private readonly IMapper _mapper;
        private protected FirebaseAuthProvider auth;
        private readonly IConfiguration _configuration;

        public FirebaseUserAuthService(IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _mapper = mapper;
            auth = new FirebaseAuthProvider(new FirebaseConfig(_configuration["FcmNotification:WebApiKey"]));
        }

        public async Task<FirebaseAuthLink> Register(Register registerationDto)
        {
            return await auth.CreateUserWithEmailAndPasswordAsync(registerationDto.Email, registerationDto.Password); ; 
        }

        public async Task<FirebaseAuthLink> Login(Login loginDto)
        {
            return await auth.SignInWithEmailAndPasswordAsync(loginDto.Email, loginDto.Password);
        }

    }
}
