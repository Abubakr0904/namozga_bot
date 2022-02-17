using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bot.Entity;
using Microsoft.Extensions.Logging;

namespace bot.Services
{
    public class InternalStorageService : IStorageService
    {
        private readonly ILogger<InternalStorageService> _logger;
        public readonly BotUser _botuser;
        private readonly Dictionary<long, BotUser> _users;

        // private readonly Dictionary<long, BotUser> _users;

        public InternalStorageService(ILogger<InternalStorageService> logger)
        {
            _users = new Dictionary<long, BotUser>();
            _logger = logger;
        }

        public Task<bool> ExistsAsync(long chatId)
            => Task.FromResult<bool>(_users.ContainsKey(chatId));

        public Task<bool> ExistsAsync(string username)
            => Task.FromResult<bool>(_users.Values.Any(u => u.Username == username));
       

        public Task<BotUser> GetUserAsync(long chatId)
            => Task.FromResult<BotUser>(_users[chatId]);
        

        public Task<BotUser> GetUserAsync(string username)
            => Task.FromResult<BotUser>(_users.Values.FirstOrDefault(u => u.Username == username));

        public async Task<(bool IsSuccess, Exception exception)> InsertUserAsync(BotUser user)
        {
            if(await ExistsAsync(user.ChatId))
            {
                return (false, new Exception("User already exists!"));
            }

            _users.Add(user.ChatId, user);
            return (true, null);
        }

        public async Task<(bool IsSuccess, Exception exception, BotUser user)> RemoveAsync(BotUser user)
        {
            if(await ExistsAsync(user.ChatId))
            {
                var savedUser = _users[user.ChatId];
                _users.Remove(user.ChatId);
                return (true, null, savedUser);
            }

            return (false, new Exception("User does not exist!"), null);
        }

        public async Task<(bool IsSuccess, Exception exception)> UpdateUserAsync(BotUser user)
        {
            if(await ExistsAsync(user.ChatId))
            {
                _users.Remove(user.ChatId);
                _users.Add(user.ChatId, user);

                return (true, null);
            }

            return (false, new Exception("User does not exist!"));
        }
        public async Task<(bool IsSuccess, Exception exception, bool status)> UpdateNotificationSettingAsync(BotUser user)
        {
            var status = true;
            if(await ExistsAsync(user.ChatId))
            {
                if(user.NotificationSetting)
                {
                    user.NotificationSetting = false;
                    status = false;
                }
                else
                {
                    user.NotificationSetting = true;
                    status = true;
                }
                _users.Remove(user.ChatId);
                _users.Add(user.ChatId, user);

                return (true, null, status);
            }

            return (false, new Exception("User does not exist!"), status);
        }

        Task<(bool IsSuccess, List<BotUser>, Exception exception)> IStorageService.GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}