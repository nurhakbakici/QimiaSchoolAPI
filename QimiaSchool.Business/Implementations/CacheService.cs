using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using QimiaSchool.Business.Abstractions;

namespace QimiaSchool.Business.Implementations;

public class CacheService : ICacheService
{
    private readonly ILogger<CacheService> _logger;
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache, ILogger<CacheService> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await _cache.GetStringAsync(key, cancellationToken);

        if ( value != null)
        {
            _logger.LogInformation("Cache successful for the key '{key}'", key);
            return JsonSerializer.Deserialize<T>(value);
        }

        _logger.LogInformation("Cache missing for the key '{key}'", key);
        return default;

        // this code block will retrive cached values by a specified key and it will deserialize it to the type we will give to it.
        // Deserializiation is the process for converting serialized data to its original form or object.
    }


    public async Task<bool> SetAsync<T>(string key, T value ,TimeSpan? expirationDate=null ,CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions();

        if (expirationDate.HasValue) // ask why the original code use .HasValue attribute on friday morning.
        {
            options.AbsoluteExpirationRelativeToNow = expirationDate.Value;
        }

        var serializedValue = JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key, serializedValue, options, cancellationToken);

        _logger.LogInformation("Cache is set for the key '{key]'", key);

        return true;

        // this method will be used to seralize and cache a value with given type. We can also give it an expiration date.
    }

    public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken=default)
    {
        var exist = await _cache.GetAsync(key, cancellationToken);
        if (exist != null)
        {
            await _cache.RemoveAsync(key, cancellationToken);
            _logger.LogInformation("Cache has been removed for the key '{key}'",key);
            return true;
        }

        _logger.LogInformation("Cache removal has failed for the key '{key}'", key);
        return false;

        // this method checks with the key we have given and if there is a key it will remove it from redis.
    }

    // those caching methods will be used in our Manager classes.
}