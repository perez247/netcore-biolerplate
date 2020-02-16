using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Application.Entities.UserEntity.Command.SignUp;
using Application.Exceptions;
using Application.Interfaces.IServices;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Implementation.Email
{
    /// <summary>
    /// EmailService implementation of IEmailService
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Constructor for EmailService
        /// </summary>
        /// <param name="logger"></param>
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
            _hostname = Environment.GetEnvironmentVariable("HOSTNAME");

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
            {
                _client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_APIKEY"));
            }
        }
        private readonly ILogger _logger;
        private readonly SendGridClient _client;
        private readonly string _hostname;

        /// <summary>
        /// writing email to file rather than sending: for development
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task WriteToFile(string data) {
            using (var writer = new StreamWriter(("emailmessage.txt"), append: false))
            {
                await writer.WriteLineAsync(data);
            }
        }

        /// <summary>
        /// Method for calling third party application to send mail
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task SendData(VerifyEmailObject data, SendGridMessage msg)
        {
            // If client was set sent email else write to the folder for development
            if (_client != null) 
            {
                msg.SetTemplateData(data);
                var result = await _client.SendEmailAsync(msg);
                // await WriteToFile(result.StatusCode.ToString());
                // throw new CustomMessageException(result.StatusCode.ToString());
            } else {
                // await WriteToFile(data.Url);
                throw new CustomMessageException(data.Url);
            }
        }
        // ------------------------------------------------

        /// <summary>
        /// Send Email verification link to use and verify email 
        /// </summary>
        /// <param name="verifyEmailData"></param>
        /// <returns></returns>
        public async Task SendVerifyEmailAsync(EmailData verifyEmailData)
        {
            // Send Grid template for verify email
            string VerifyEmailTemplateId = "d-ba9d004d434d49bfa5f7b137210c548e";

            var msg = EmailFunctions.GenerateMsg("noreply@fortheeco.com", "Fortheeco",VerifyEmailTemplateId, verifyEmailData.User.Email);
            
            string token = WebUtility.UrlEncode(verifyEmailData.Token);
            string id = WebUtility.UrlEncode(verifyEmailData.User.Id.ToString());

            var data = new VerifyEmailObject() {
                FirstName = verifyEmailData.User.UserDetail.FirstName,
                Url = $"{_hostname}/public/verify-email?token={token}&userId={id}"
            };

            await SendData(data, msg);
        }

        /// <summary>
        /// Send a link for user to use and change password
        /// </summary>
        /// <param name="verifyEmailData"></param>
        /// <returns></returns>
        public async Task SendForgotPasswordEmailAsync(EmailData verifyEmailData)
        {
            var templateId = "d-dbaf10c1b0184df6af7d188f333bd863";

            var msg = EmailFunctions.GenerateMsg("noreply@fortheeco.com", "Fortheeco", templateId, verifyEmailData.User.Email);

            string token = WebUtility.UrlEncode(verifyEmailData.Token);
            string id = WebUtility.UrlEncode(verifyEmailData.User.Id.ToString());
            
            var data = new VerifyEmailObject() {
                FirstName = verifyEmailData.User.UserDetail.FirstName,
                Url = $"{_hostname}/public/reset-password?token={token}&userId={id}"
            }; 

            await SendData(data, msg);
        }

        /// <summary>
        /// Send a notification to the user of newly changed password
        /// </summary>
        /// <param name="VerifyEmailData"></param>
        /// <returns></returns>
        public async Task SendChangePasswordNotificationAsync(EmailData VerifyEmailData)
        {
            var templateId = "d-8dbe4d32d3c142ff97717ee4609073b9";
            
            var msg = EmailFunctions.GenerateMsg("noreply@fortheeco.com", "Fortheeco", templateId, VerifyEmailData.User.Email);

            string token = WebUtility.UrlEncode(VerifyEmailData.Token);
            string id = WebUtility.UrlEncode(VerifyEmailData.User.Id.ToString());
            
            var data = new VerifyEmailObject() {
                FirstName = VerifyEmailData.User.UserDetail.FirstName,
                Url = $"Notification sent: developers only"
            };

            await SendData(data, msg);
        }
    }
}


