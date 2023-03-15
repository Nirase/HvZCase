using HvZAPI.Models;

namespace HvZAPI.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Fetches all Users
        /// </summary>
        /// <returns>Enumerable of Users</returns>
        Task<IEnumerable<User>> GetUsers();

        /// <summary>
        /// Fetches a User in a game based on id
        /// </summary>
        /// <returns> User entity </returns>
        Task<User> GetUserById(int UserId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        Task<User> AddUser(User User);

        /// <summary>
        /// Fetches a User in a game based on id
        /// </summary>
        /// <returns> User entity </returns>
        Task<User> UpdateUser(User User);

        /// <summary>
        /// deketes a User from a game based on id
        /// </summary>
        /// <returns> deletes User entity </returns>
        Task DeleteUser(int UserId);
    }
}
