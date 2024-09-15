
namespace Affection.Infrastructure.Implementation;
public class CacheService : ICacheService
{

    private readonly IDatabase _cacheDatabase;
    public CacheService()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");

        _cacheDatabase = redis.GetDatabase();
    }
    public T GetData<T>(string key)
    {
        var value = _cacheDatabase.StringGet(key);

        if (!string.IsNullOrEmpty(value))
            return JsonSerializer.Deserialize<T>(value);

        return default;
    }

    public object RemoveData(string key)
    {
        var _exit = _cacheDatabase.KeyExists(key);

        if (_exit) 
          return  _cacheDatabase.KeyDelete(key);

        return false;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);

        return _cacheDatabase.StringSet(key ,JsonSerializer.Serialize(value) , expiryTime);

    }
}
