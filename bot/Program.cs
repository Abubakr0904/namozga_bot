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
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Configuration;

namespace bot
{
    class Program
    {
        // public static IConfigurationRoot Configuration { get; private set; }

        static Task Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .RunAsync();

        private static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureServices(Configure);
                // .ConfigureAppConfiguration((context, configuration) =>
                // {
                //     configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                //     // Configuration = configuration.Build();
                // });

        private static void Configure(HostBuilderContext context, IServiceCollection services)
        {
            services.AddDbContext<BotDbContext>(
                options =>
                    options.UseNpgsql("User ID=tmihppdyrytscb;Password=a7fd6415ef24e0c7e7a13d57e6c065c8b4e0934d3371f2b773268303efac7ebd;Host=ec2-52-204-196-4.compute-1.amazonaws.com;Port=5432;Database=d67cinktj4b7oe;Pooling=true;Integrated Security=true;sslmode=Require;Trust Server Certificate=true;"), ServiceLifetime.Singleton);
                // options.UseSqlite("Data Source=bot.db;"), ServiceLifetime.Singleton);
            services.AddMemoryCache();
            services.AddSingleton<TelegramBotClient>(b => new TelegramBotClient("2010387651:AAEjEk7F0FaEp5GW2FbYr5qHzyUFvxwPyr4"));
            
            services.AddHostedService<Bot>();
            services.AddHostedService<NotificationBackgroundService>();
            
            // services.AddTransient<IStorageService, InternalStorageService>();
            services.AddTransient<IStorageService, DbStorageService>();
            services.AddTransient<Handlers>();
            services.AddHttpClient<IPrayerTimeService, AladhanClient>(
                client => 
                {
                    client.BaseAddress = new Uri("http://api.aladhan.com/v1");
                });
            services.AddTransient<ICacheService, PrayerTimeCacheService>(); 
            // services.AddTransient<BotUser>();
            // services.AddTransient<IStorageService, DbStorageService>();
        }
    }
}
