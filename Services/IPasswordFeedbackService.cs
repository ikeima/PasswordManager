using PasswordManager.Models;

namespace PasswordManager.Services
{
    public interface IPasswordFeedbackService
    {
        Task<int> GetCountAsync();
        Task<string> GetPasswordAsync(string password);
        Task<string> GetHashByFeedbackAsync(string feedback);
        Task<bool> AddNewCombinationAsync(PasswordFeedback combination);
    }
}
