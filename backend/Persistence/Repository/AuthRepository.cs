using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Entities.UserEntity.Command.SignUp;
using Application.Interfaces.IServices;
using Application.Entities.UserEntity.Query.SendConfirmEmail;
using Application.Exceptions;
using System;

namespace Persistence.Repository
{
    /// <summary>
    /// This is the class that implements the IAuthRepository Interface
    /// Contains all the authentication and authorization
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DefaultDataContext _dataContext;

        /// <summary>
        /// COntructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="dataContext"></param>
        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager, DefaultDataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
        }

        /// <summary>
        /// create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<EmailData> SignUp(User user, string Password) {
            var result = await _userManager.CreateAsync(user, Password);

            if (!result.Succeeded)
                return new EmailData() {
                Errors = result.Errors.FirstOrDefault().Description,
                // Errors = string.Join(',', result.Errors.Select(x=>x.Description)),
                User = null
            };
            
            user = await CreateUserDetailsAndContacts(user);
            
            var EmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            
            return new EmailData() { Errors = null, User = user, Token = EmailToken };

        }

        /// <summary>
        /// Sign in a user
        /// </summary>
        /// <param name="userNameOrEmail"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<User> SignIn(string userNameOrEmail, string Password) {

            // Get the user form the database
            var user = _userManager.Users.FirstOrDefault(u => u.Email.ToUpper() == userNameOrEmail.ToUpper());

            // If no user is found then return invalid credentials
            if (user == null)
                throw new CustomMessageException("invalid login credentials now");

            // If username and password combo is correct then proceed
            if (await _userManager.CheckPasswordAsync(user, Password)) {

                // If user has confirmed emails proceed
                if (user.EmailConfirmed)
                    return user;

                // If email is not confirmed then throw error, user must verify email first
                throw new ConfirmEmailException(user.Email);
            }

            // If user is currently locked out let them know
            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value.UtcDateTime.ToUniversalTime() > DateTime.Now.ToUniversalTime())
                throw new CustomMessageException($"This account has been locked for now");

            // Increase failed attempts
            await _userManager.AccessFailedAsync(user);

            // Throw invalid login agian
            throw new CustomMessageException("invalid login credentials unsuss");
        }

        public async Task<bool> VerifyEmail(string id, string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id.ToString() == id);

            if (user == null)
                return false;

            if (await _userManager.IsEmailConfirmedAsync(user))
                throw new CustomMessageException($"{user.Email} has already been verified");

            var result = await _userManager.ConfirmEmailAsync(user,token);

            return result.Succeeded;
        }

        /// <summary>
        /// Hanldes checking if the user exist and email has been confirmed already
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<EmailData> SendVerificationEmail(string email) 
        {
            // Gets the user from the data
            var user = await _userManager.Users.Include(u => u.UserDetail).SingleOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper());

            // if no user throw an error
            if (user == null)
                throw new CustomMessageException($"{email} isn't part of ECO yet :)");

            // if user has been verified throw another error
            if (await _userManager.IsEmailConfirmedAsync(user))
                throw new CustomMessageException($"{email} has already been confirm" );

            // If user has not, generate new token to be sent
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // return user and token
            return new EmailData() {
                User = user,
                Token = token
            };
        }

        /// <summary>
        /// Check if email has not been taken by another user
        /// </summary>
        /// <param name="usernameOrEmail"></param>
        /// <returns></returns>
        public async Task<bool> EmailAvailable(string usernameOrEmail)
        {
            if (string.IsNullOrEmpty(usernameOrEmail))
                throw new CustomMessageException("Cannot lookup email, please try again later");

            var user = await _userManager.Users.SingleOrDefaultAsync(
                            u => u.NormalizedEmail == usernameOrEmail.ToUpper() || u.NormalizedUserName == usernameOrEmail.ToUpper());

            return user == null;
        }

        public async Task<EmailData> SendForgotPasswordEmail(string email)
        {
            var user = await _userManager.Users.Include(u => u.UserDetail).SingleOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper());

            if (user == null)
                return new EmailData(){ Errors = $"{email} has not been signed up to the ECO platform" };

             var token = await _userManager.GeneratePasswordResetTokenAsync(user);

             
            return new EmailData() {
                User = user,
                Token = token
            };
        }

        /// <summary>
        /// Verifies users and token then change password
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<EmailData> VarifyAndChangeUserPassword(string token, string userId, string password)
        {
            // Check the database to see if user exists on the platform
            var user = await _userManager.Users.Include(u => u.UserDetail).FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            // if user does not exists then throw error occured
            if (user == null)
                throw new CustomMessageException("An error occurred, please try again later");
            
            // Try resetting password
            var result = _userManager.ResetPasswordAsync(user, token, password).Result;

            // If failed throw error with first error message
            if (!result.Succeeded) 
                throw new CustomMessageException(result.Errors.FirstOrDefault().Description);

            // return the new user if all goes well
            return new EmailData() { User = user };
        }

        private async Task<User> CreateUserDetailsAndContacts(User user)
        {
            var contact = new Contact() {
                Type = nameof(User),
                TypeId = user.Id,
                Email_1 = user.Email
            };

            _dataContext.Contacts.Add(contact);

            await _dataContext.SaveChangesAsync();

            return user;
        }
    }
} 