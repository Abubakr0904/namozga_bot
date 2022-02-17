using System.Linq;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using bot.Entity;
using bot.HttpClients;
using bot.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using bot.Extensions;
using GeoTimeZone;
using TimeZoneConverter;
using bot.Models;

namespace bot
{
    public class NotificationBackgroundService : BackgroundService, IHostedService, IDisposable
    {
        private readonly ILogger<Handlers> _logger;
        private readonly IStorageService _storage;
        private readonly IPrayerTimeService _client;
        private readonly ITelegramBotClient _botclient;
        private readonly ICacheService _cache;
        public NotificationBackgroundService(ICacheService cache, TelegramBotClient botclient, ILogger<Handlers> logger, IStorageService storage, IPrayerTimeService client)
        {
            _logger = logger;
            _storage = storage;
            _client = client;
            _botclient = botclient;
            _cache = cache;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
        //     while(true)
        //     {
        //         var users = await _storage.GetAllUsersAsync();
        //         if(users.IsSuccess)
        //         {
        //             foreach (var user in users.Item2)
        //             {
        //                 if(user.NotificationSetting)
        //                 {
        //                     var expirationUser = await ToUtcAsync(user, user.Latitude, user.Longitude);
                            
        //                     var usersTime = $"{expirationUser.Item2.Day.ToString()}, {expirationUser.Item2.Hour.ToString()}, {expirationUser.Item2.Minute.ToString()}";
        //                     var SystemTime = $"{DateTime.UtcNow.Day.ToString()}, 15, 23";
        //                     Console.WriteLine($"{usersTime} --- {SystemTime}");
                                                
        //                     if(expirationUser.Item1.Latitude != 400 && usersTime == SystemTime )
        //                     {
        //                         await _botclient.SendTextMessageAsync(
        //                             chatId: expirationUser.Item1.ChatId,
        //                             parseMode: ParseMode.Markdown,
        //                             text: _cache.GetOrUpdatePrayerTimeAsync(expirationUser.Item1.ChatId, user.Latitude, user.Longitude).Result.prayerTime.TimeToString(user.Language),
        //                             replyMarkup: MessageBuilder.LocationRequestButton(user.Language)
        //                         );
        //                     }
        //                 }
        //             }
        //             Thread.Sleep(60000);
        //         }
        //         else
        //         {
        //             _logger.LogCritical("Error with notification service");
        //         }
        //     }
        // }
        // private async Task<(BotUser user, DateTimeOffset expiration)> ToUtcAsync(BotUser user, double latitude, double longitude)
        // {
        //     string tzIana = TimeZoneLookup.GetTimeZone(latitude, longitude).Result;
        //     TimeZoneInfo tzInfo = TZConvert.GetTimeZoneInfo(tzIana);
            
        //     DateTimeOffset convertedTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzInfo);
            
        //     return (user, convertedTime);
        }
    }
}