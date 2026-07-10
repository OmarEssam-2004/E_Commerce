using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Contracts
{
    public interface IAuthenticationService
    {
       Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);
       Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default);
    }
}
