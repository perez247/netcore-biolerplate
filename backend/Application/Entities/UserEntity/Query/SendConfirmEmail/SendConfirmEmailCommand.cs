using System.Threading;
using System.Threading.Tasks;
using Application.Entities.UserEntity.Command.SignUp;
using Application.Exceptions;
using Application.Infrastructure.Validations;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Entities.UserEntity.Query.SendConfirmEmail
{

    /// <summary>
    /// class responsible for collecting send verification email data
    /// </summary>
    public class SendConfirmEmailCommand : CaptchaCredentials, IRequest<Unit>
    {
        /// <summary>
        /// Email address to send data
        /// </summary>
        /// <value>string</value>
        public string EmailAddress { get; set; }
    }

    /// <summary>
    /// Class to handle the request
    /// </summary>
    public class SendConfirmEmailHandler : IRequestHandler<SendConfirmEmailCommand, Unit>
    {
        private readonly IAuthRepository _auth;
        private readonly IMediator _mediator;

        /// <summary>
        /// The contructor
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="mediator"></param>
        public SendConfirmEmailHandler(IAuthRepository auth, IMediator mediator)
        {
            _auth = auth;
            _mediator = mediator;
        }

        /// <summary>
        /// The method called within the class to handle the request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            // errors are thrown within the application
            var result = await _auth.SendVerificationEmail(request.EmailAddress);

            await _mediator.Publish(new VerifyEmailTokenCreated() { VerifyEmailData = result });

            return Unit.Value;
        }
    }

}