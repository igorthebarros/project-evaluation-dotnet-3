namespace SharedKernel.Interfaces
{
    public interface IBaseRespository<T> where T : class
    {
        /// <summary>
        /// Creates a new entity in the repository
        /// </summary>
        /// <param name="T">The entity to be created</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created entity</returns>
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a entity in the repository based on it's Guid Id
        /// </summary>
        /// <param name="id">Entity Id on Guid format</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The found entity</returns>
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve all entities
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of entities</returns>
        Task<IReadOnlyList<T>?> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a entity in the repository based on it's Guid Id
        /// </summary>
        /// <param name="T">The entity to be updated</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated entity</returns>
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a entity in the repository
        /// </summary>
        /// <param name="id">Entity Id on Guid format</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A bool confirming if the task was successful or not</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
