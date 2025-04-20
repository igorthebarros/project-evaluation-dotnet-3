using Microsoft.EntityFrameworkCore;
using SharedKernel.Common;
using SharedKernel.Interfaces;

namespace ORM.Repositories
{
    /// <summary>
    /// Implementation of IUserRepository using Entity Framework Core
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of UserRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public UserRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new user in the database
        /// </summary>
        /// <param name="user">The user to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created user</returns>
        public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        /// <summary>
        /// Retrieves a user by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The user if found, null otherwise</returns>
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _context.Users.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            if (result == null)
                throw new Exception($"User of Id {id} was not found.");

            return result;
        }

        /// <summary>
        /// Retrieves a user by their email address
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The user if found, null otherwise</returns>
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var result = await _context.Users.FirstOrDefaultAsync(o => o.Email == email, cancellationToken);

            if (result == null)
                throw new Exception($"User of email {email} was not found.");

            return result;
        }

        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the user was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await GetByIdAsync(id, cancellationToken);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public Task<IReadOnlyList<User>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken = default)
        {
            var user = await GetByIdAsync(entity.Id, cancellationToken).ConfigureAwait(false);
            if (user == null)
                await _context.Users.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            else
                _context.Users.Update(entity);

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        }
    }
}
