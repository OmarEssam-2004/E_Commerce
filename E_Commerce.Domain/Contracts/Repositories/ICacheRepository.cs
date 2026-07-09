using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts.Repositories
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string Key, CancellationToken ct = default);
        Task SetAsync(string Key, object value, TimeSpan? duration = default, CancellationToken ct = default);
        
    }
}
