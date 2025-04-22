using SharedKernel.Common;
using SharedKernel.Interfaces;

namespace Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for User entity operations
    /// </summary>
    public interface IUserRepository : IBaseRespository<User>
    {
        /// <summary>
        /// Retrieves a user by their email address
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The user if found, null otherwise</returns>
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
