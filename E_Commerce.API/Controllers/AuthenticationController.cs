using E_Commerce.Application.DTOS.Identity;
using E_Commerce.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace E_Commerce.API.Controllers
{
    public class AuthenticationController(IAuthenticationService authenticationService) : ApiBaseController
    {
        [HttpGet("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto, CancellationToken ct = default)
        {
            var result = await authenticationService.LoginAsync(loginDto, ct);
            return ToActionResult(result);
        }

        [HttpGet("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, CancellationToken ct = default)
        {
            var result = await authenticationService.RegisterAsync(registerDto, ct);
            return ToActionResult(result);
        }
    }
}
