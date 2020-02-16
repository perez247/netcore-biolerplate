using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Token;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Entities.UserEntity.Query.GetUserDetailCommand
{
    public class GetUserDetailCommand : TokenCredentials, IRequest<UserDetailDto>
    {
        public Guid Id { get; set; }
    }

    public class GetUsersABoutMeHandler : IRequestHandler<GetUserDetailCommand, UserDetailDto>
    {

        private readonly IUnitOfWork _unitOfWork;
        public GetUsersABoutMeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserDetailDto> Handle(GetUserDetailCommand request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.User.GetUserAboutMe(request.Id);
            return UserDetailDto.Create(response);
        }
    }
}