using System.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bot.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace bot.Services
{
    public class DbStorageService : IStorageService
    {
        private readonly BotDbContext _context;

        public DbStorageService(BotDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistsAsync(long chatId)
            => await _context.Users.AnyAsync(u => u.ChatId == chatId);

        public async Task<bool> ExistsAsync(string username)
            => await _context.Users.AnyAsync(u => u.Username == username);

        public async Task<BotUser> GetUserAsync(long chatId)
            => await _context.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);

        public async Task<BotUser> GetUserAsync(string username)
            => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        public async Task<(bool IsSuccess, Exception exception)> InsertUserAsync(BotUser user)
        {
            try
            {
                if(!await ExistsAsync(user.ChatId))
                {
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return(true, null);
                }
                return (false, null);
            }
            catch (Exception e)
            {
                return (false, e);
            }
        }
        public async Task<(bool IsSuccess, Exception exception, BotUser user)> RemoveAsync(BotUser user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return(true, null, user);
            }
            catch (Exception e)
            {
                return (false, e, null);
            }
        }

        public async Task<(bool IsSuccess, Exception exception, bool status)> UpdateNotificationSettingAsync(BotUser user)
        {
            if(user.NotificationSetting) user.NotificationSetting = false;
            else user.NotificationSetting = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            
            return (true, null, user.NotificationSetting);
        }

        public async Task<(bool IsSuccess, Exception exception)> UpdateUserAsync(BotUser user)
        {
            try
            {
                    var savedUser = await GetUserAsync(user.ChatId);
                    _context.Users.Remove(savedUser);
                    await _context.SaveChangesAsync();
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return(true, null);
            }
            catch (Exception e)
            {
                return (false, e);
            }
        }
        async Task<(bool IsSuccess, List<BotUser>, Exception exception)> IStorageService.GetAllUsersAsync()
        {
            try
            {
                List<BotUser> userslist = new List<BotUser>();
                foreach (var user in _context.Users)
                {
                    userslist.Add(user);
                }
                return (true, userslist, null);
            }
            catch (System.Exception e)
            {
                 return (false, null, e);
            }
        }
    }
}