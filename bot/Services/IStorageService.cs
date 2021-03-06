using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bot.Entity;

namespace bot.Services
{
    public interface IStorageService
    {
        Task<(bool IsSuccess, Exception exception)> InsertUserAsync(BotUser user);
        Task<(bool IsSuccess, Exception exception)> UpdateUserAsync(BotUser user);
        Task<bool> ExistsAsync(long chatId);
        Task<bool> ExistsAsync(string username);
        Task<(bool IsSuccess, Exception exception, BotUser user)> RemoveAsync(BotUser user);
        Task<(bool IsSuccess, Exception exception, bool status)> UpdateNotificationSettingAsync(BotUser user);
        Task<BotUser> GetUserAsync(long chatId);
        Task<BotUser> GetUserAsync(string username);
        Task<(bool IsSuccess, List<BotUser>, Exception exception)> GetAllUsersAsync();
    }
}