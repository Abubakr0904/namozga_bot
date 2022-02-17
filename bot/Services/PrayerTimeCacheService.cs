using System;
using System.Threading.Tasks;
using bot.HttpClients;
using bot.Models;
using Microsoft.Extensions.Caching.Memory;

namespace bot.Services
{
    public class PrayerTimeCacheService : ICacheService
    {
        private readonly IMemoryCache _memCache;
        private readonly IPrayerTimeService _client;

        public PrayerTimeCacheService(  IMemoryCache memCache,
                                        IPrayerTimeService client)
        {
            _memCache = memCache;
            _client = client;
        }

        public async Task<(bool IsSuccess, PrayerTime prayerTime, Exception exception)> GetOrUpdatePrayerTimeAsync(long chatId, double latitude, double longitude)
        {
            var key = String.Format($"{chatId}:{latitude}:{longitude}");
            return await _memCache.GetOrCreateAsync(key, async entry => 
                {
                    var result = await _client.GetPrayerTimeAsync(latitude, longitude);
                    var zone = result.prayerTime.Timezone;
                    var zoneId = TimeZoneInfo.FindSystemTimeZoneById(zone);
                    var expirationTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse("23:59"), zoneId);
                    entry.AbsoluteExpiration = expirationTime;
                    
                    return result;
                }
            );
        }
    }
}