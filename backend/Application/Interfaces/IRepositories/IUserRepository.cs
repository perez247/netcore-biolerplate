using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    /// <summary>
    /// Repository for handling command and queries for the user entity (domain)
    /// </summary>
    public interface IUserRepository
    {

        /// <summary>
        /// Get the user's details 
        /// </summary>
        /// <param name="UserId"> Guid of the user </param>
        /// <returns></returns>
        Task<UserDetail> GetUserAboutMe(Guid UserId);
    }
}