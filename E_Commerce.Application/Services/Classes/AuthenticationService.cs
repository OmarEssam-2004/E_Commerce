using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Identity;
using E_Commerce.Application.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Application.Services.Classes
{
    public class AuthenticationService(IIdentityService identityService, ITokenService tokenService) : IAuthenticationService
    {
        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {

            var userResult = await identityService.FindUserByEmailAsync(loginDto.Email, ct);
            if (!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);

            var passwordResult = await identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);
            if (!passwordResult.IsSuccess)
                return Result<UserDto>.Fail(passwordResult.Errors);


            var roleResult = await identityService.GetUserRolesAsync(userResult.Data.Email, ct);

            var token = await tokenService.CreateTokenAsync(userResult.Data.Id, userResult.Data.Email, userResult.Data.UserName, roleResult.Data);

            var user = userResult.Data;

            return Result<UserDto>.Ok(new UserDto()
            {
                Email = user.Email,
                Token = token,
                DisplayName = user.DisplayName

            });


        }
        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var createResult = await identityService.CreateUserAsync(registerDto, ct);
            if (!createResult.IsSuccess)
                return Result<UserDto>.Fail(createResult.Errors);


            var roleResult = await identityService.GetUserRolesAsync(createResult.Data.Email, ct);

            var token = await tokenService.CreateTokenAsync(createResult.Data.Id, createResult.Data.Email, createResult.Data.UserName, roleResult.Data);

            var user = createResult.Data;
            return Result<UserDto>.Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            });

        }
    }
}
