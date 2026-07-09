using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Contracts
{
    public interface ICacheService
    {
        Task<string> GetAsync(string Key, CancellationToken ct = default);
        Task SetAsync(string Key,object value,TimeSpan? duration = null ,CancellationToken ct = default);
    }
}
