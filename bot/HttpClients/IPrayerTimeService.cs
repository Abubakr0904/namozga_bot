using System;
using System.Threading.Tasks;
using bot.Models;

namespace bot.HttpClients
{
    public interface IPrayerTimeService
    {
        Task<(bool IsSuccess, PrayerTime prayerTime, Exception exception)> GetPrayerTimeAsync(double latitude, double longitude);
    }
}