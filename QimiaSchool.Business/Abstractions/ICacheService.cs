using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Abstractions;

public interface ICacheService
{
    Task<T> GetAsync<T>(string key, CancellationToken cancellationToken=default);
    Task<bool> SetAsync<T>(string key, T value, TimeSpan? expirationDate = null, CancellationToken cancellationToken=default);
    Task<bool> RemoveAsync(string key, CancellationToken cancellationToken=default);
}
