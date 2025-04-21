using Human_Benchmark_2._0.Interaces;
using Microsoft.Extensions.Caching.Memory;

namespace Human_Benchmark_2._0.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void HandleCacheAdmin(int countUsers, int countUsersByPage)
        {
            if (!_memoryCache.TryGetValue("pageCount", out int cachedPageCount)) // Checks if pageCount has been deleted (after user deletion)
            {
                if (_memoryCache.TryGetValue("count", out int cachedUsers)) // Checks if count exists which if true means that a user has been deleted
                {
                    int pageAllFromCachedUsers = (int)Math.Ceiling((double)cachedUsers / countUsersByPage);
                    for (int i = 1; i <= pageAllFromCachedUsers; i++) // Removes all pages which are calculated with the previous user count because if they are calculated with the present user count, some pages might not get deleted
                    {
                        _memoryCache.Remove($"Page:{i}");
                    }
                }
            }
            else
            {
                if (_memoryCache.TryGetValue("count", out int cachedUsers) && cachedUsers != countUsers) // Checks if a user has registered or deleted an account by themselves
                {
                    for (int i = 1; i <= cachedPageCount; i++) // Deletes every page in case an account is missing
                    {
                        _memoryCache.Remove($"Page:{i}");
                    }
                }
            }
        }
    }
}
