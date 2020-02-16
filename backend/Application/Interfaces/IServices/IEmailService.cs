using System.Threading.Tasks;
using Application.Entities.UserEntity.Command.SignUp;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
    /// <summary>
    /// Interface responsible for sending email
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send Email verification link to use and verify email 
        /// </summary>
        /// <param name="VerifyEmailData"></param>
        /// <returns></returns>
        Task SendVerifyEmailAsync(EmailData VerifyEmailData);

        /// <summary>
        /// Send a link for user to use and change password
        /// </summary>
        /// <param name="VerifyEmailData"></param>
        /// <returns></returns>
        Task SendForgotPasswordEmailAsync(EmailData VerifyEmailData);

        /// <summary>
        /// Send a notification to the user of newly changed password
        /// </summary>
        /// <param name="VerifyEmailData"></param>
        /// <returns></returns>
        Task SendChangePasswordNotificationAsync(EmailData VerifyEmailData);

    }

    /// <summary>
    /// Data structure to be used when sending email
    /// </summary>
    public class EmailData {
        /// <summary>
        /// User to send data to
        /// </summary>
        /// <value></value>
        public User User { get; set; }

        /// <summary>
        /// Token for verification if avaliable
        /// </summary>
        /// <value></value>
        public string Token { get; set; }

        /// <summary>
        /// Errors to be displayed to the user: used in the command class
        /// </summary>
        /// <value></value>
        public string Errors { get; set; }
    }
}