using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities.CoreEntities;
using MediatR;

namespace Application.Entities.LocationEntity.Query.GetCountries
{
    public class GetCountriesCommand : IRequest<ICollection<CountriesDTO>>
    { }

    public class GetCountriesHandler : IRequestHandler<GetCountriesCommand, ICollection<CountriesDTO>>
    {
        public IMapper _mapper { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public GetCountriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<CountriesDTO>> Handle(GetCountriesCommand request, CancellationToken cancellationToken)
        {   
            return _mapper.Map<ICollection<Country>, ICollection<CountriesDTO>>(await _unitOfWork.Location.GetCountries());
        }
    }
}