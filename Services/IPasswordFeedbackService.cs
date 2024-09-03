using PasswordManager.Models;

namespace PasswordManager.Services
{
    public interface IPasswordFeedbackService
    {
        /// <summary>
        /// Возвращает количество записей в словаре комбинаций
        /// </summary>
        /// <returns>Количество записей в словаре комбинаций</returns>
        Task<int> GetCountAsync();
        /// <summary>
        /// На запрос с паролем возвращает связанный с ним отзыв, после каждого символа которого добавлен знак пробела
        /// </summary>
        /// <param name="password">Пароль используется как ключ</param>
        Task<string> GetPasswordAsync(string password);
        /// <summary>
        /// На запрос с отзывом возвращает связанный с ним хэш пароля <see cref="ComputeHash(string)"/>
        /// </summary>
        /// <param name="feedback">Отзыв используется для поиска значения пароля</param>
        Task<string> GetHashByFeedbackAsync(string feedback);
        /// <summary>
        /// Добавляет новую запись "ключ-значение"
        /// </summary>
        /// <param name="combination">Получаемое от пользователя комбинация "ключ-значение"</param>
        Task<bool> AddNewCombinationAsync(PasswordFeedback combination);
    }
}
