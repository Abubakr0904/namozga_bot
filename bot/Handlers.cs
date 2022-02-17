using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using bot.Entity;
using bot.Services;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using bot.Extensions;

namespace bot
{
    public partial class Handlers
    {
        private readonly ILogger<Handlers> _logger;
        private readonly IStorageService _storage;
        private readonly ICacheService _cache;
        private readonly double _latitude;
        private readonly double _longitude;
        public Handlers(ILogger<Handlers> logger, IStorageService storage, ICacheService cache)
        {
            _logger = logger;
            _storage = storage;
            _cache = cache;
        }

        public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken ctoken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException => $"Error occured with Telegram Client: {exception.Message}",
                _ => exception.Message
            };

            _logger.LogCritical(errorMessage);

            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken ctoken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(client, update.Message),
                UpdateType.EditedMessage => BotOnMessageEdited(client, update.EditedMessage),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(client, update.CallbackQuery),
                UpdateType.InlineQuery => BotOnInlineQueryReceived(client, update.InlineQuery),
                UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(client, update.ChosenInlineResult),
                _ => UnknownUpdateHandlerAsync(client, update)
            };

            try
            {
                await handler;
            }
            catch(Exception e)
            {

            }
        }

                        
        private async Task BotOnMessageEdited(ITelegramBotClient client, Message editedMessage)
        {
            _logger.LogInformation($"Message edited: {editedMessage}");
        }

        private async Task UnknownUpdateHandlerAsync(ITelegramBotClient client, Update update)
        {
            _logger.LogInformation($"Unknown update: {update}");
        }

        private async Task BotOnChosenInlineResultReceived(ITelegramBotClient client, ChosenInlineResult chosenInlineResult)
        {
            _logger.LogInformation($"Inline result received: {chosenInlineResult}");
        }

        private async Task BotOnInlineQueryReceived(ITelegramBotClient client, InlineQuery inlineQuery)
        {
            _logger.LogInformation($"Inline button pushed: {inlineQuery}");
        }

        private async Task BotOnCallbackQueryReceived(ITelegramBotClient client, CallbackQuery callbackQuery)
        {
            _logger.LogInformation($"Callback query received: {callbackQuery}");
        }
    }
}