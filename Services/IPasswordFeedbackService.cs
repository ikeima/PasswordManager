namespace PasswordManager.Services
{
    public interface IPasswordFeedbackService
    {
        Task<int> GetCountAsync();
        Task<string> GetPasswordAsync();
        Task<string> GetHashByFeedbackAsync();
        Task<bool> AddNewCombinationAsync();
    }
}
