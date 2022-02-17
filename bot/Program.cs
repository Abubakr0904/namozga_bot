using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using bot.Services;
using bot.HttpClients;
using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Http;
// using bot.Services;
using bot.Entity;
using Microsoft.Extensions.Configuration;

namespace bot
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static Task Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .RunAsync();

        private static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureServices(Configure)
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    Configuration = configuration.Build();
                });

        private static void Configure(HostBuilderContext context, IServiceCollection services)
        {
            services.AddDbContext<BotDbContext>(
                options =>
                options.UseSqlite(Configuration.GetConnectionString("BotConnection")), ServiceLifetime.Singleton);
            services.AddMemoryCache();
            services.AddSingleton<TelegramBotClient>(b => new TelegramBotClient(Configuration.GetSection("Bot:Token").Value));
            
            services.AddHostedService<Bot>();
            services.AddHostedService<NotificationBackgroundService>();
            
            // services.AddTransient<IStorageService, InternalStorageService>();
            services.AddTransient<IStorageService, DbStorageService>();
            services.AddTransient<Handlers>();
            services.AddHttpClient<IPrayerTimeService, AladhanClient>(
                client => 
                {
                    client.BaseAddress = new Uri(Configuration.GetSection("Aladhan:BaseUrl").Value);
                });
            services.AddTransient<ICacheService, PrayerTimeCacheService>(); 
            // services.AddTransient<BotUser>();
            // services.AddTransient<IStorageService, DbStorageService>();
        }
    }
}
