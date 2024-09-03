using PasswordManager.Models;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Services
{
    public class PasswordFeedbackService : IPasswordFeedbackService
    {
        private static ConcurrentDictionary<string, string> _combinations = new();
        private readonly IDatabase _redisDb;

        public PasswordFeedbackService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }
        public async Task<bool> AddNewCombinationAsync(PasswordFeedback combination)
        {
            if (!_combinations.TryAdd(combination.Password, combination.Feedback))
                return false;

            await _redisDb.StringSetAsync(combination.Password, combination.Feedback);
            return true;
        }

        public Task<int> GetCountAsync()
        {
            return Task.FromResult(_combinations.Count);
        }

        public async Task<string> GetHashByFeedbackAsync(string feedback)
        {
            var server = _redisDb.Multiplexer.GetServer(_redisDb.Multiplexer.GetEndPoints().First());
            foreach (var key in server.Keys())
            {
                if (await _redisDb.StringGetAsync(key) == feedback)
                {
                    using (var sha256 = SHA256.Create())
                    {
                        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(feedback));
                        return Convert.ToBase64String(bytes);
                    }
                }
            }
            return null;
        }

        public async Task<string> GetPasswordAsync(string password)
        {
            var feedback = await _redisDb.StringGetAsync(password);
            if (feedback.HasValue)
                return string.Join(" ", feedback.ToString().ToCharArray());
            return null;
        }
    }
}
