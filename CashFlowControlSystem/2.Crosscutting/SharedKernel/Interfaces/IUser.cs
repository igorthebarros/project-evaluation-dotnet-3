namespace SharedKernel.Interfaces
{
    /// <summary>
    /// Define a representation contract for User in the system.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Define the unique identifier of a user.
        /// </summary>
        /// <returns>The user Id as a string.</returns>
        public string Id { get; }

        /// <summary>
        /// Define the username of a user.
        /// </summary>
        /// <returns>The username as a string.</returns>
        public string Username { get; }

        /// <summary>
        /// Define the user role in the system.
        /// </summary>
        /// <returns>The user role as a string.</returns>
        public string Role { get; }
    }
}