using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities.CoreEntities;
using MediatR;

namespace Application.Entities.LocationEntity.Query.GetStates
{
    public class GetStateCommand : IRequest<ICollection<StateDTO>>
    {
        public string CountryId { get; set; }
    }

    public class GetStateHandler : IRequestHandler<GetStateCommand, ICollection<StateDTO>>
    {
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public GetStateHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        async Task<ICollection<StateDTO>> IRequestHandler<GetStateCommand, ICollection<StateDTO>>.Handle(GetStateCommand request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ICollection<State>, ICollection<StateDTO>>(await _unitOfWork.Location.GetStates(request.CountryId));
        }
    }
}