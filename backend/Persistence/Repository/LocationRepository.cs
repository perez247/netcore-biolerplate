using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.IRepositories;
using Domain.Entities.CoreEntities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public class LocationRepository : ILocationRepository
    {
        public readonly int _allState = 247;
        public DefaultDataContext _dataContext { get; set; }
        public LocationRepository(DefaultDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<ICollection<Country>> GetCountries()
        {
            // await Task.WhenAll();
            // return null;
            return await _dataContext.Countries
                    .Where(c => c.Id != _allState)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
        }

        public async Task<ICollection<State>> GetStates(string CountryId)
        {
            // await Task.WhenAll();
            // return null;
            return await _dataContext.States
                .Where(s => s.CountryId.ToString() == CountryId || s.CountryId == _allState)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }


    }
}