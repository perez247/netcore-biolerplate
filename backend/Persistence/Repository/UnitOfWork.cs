using System.Threading.Tasks;
using Application.Interfaces.IRepositories;
using AutoMapper;

namespace Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMapper _mapper { get; set; }
        private readonly DefaultDataContext _context;
        public ILocationRepository Location { get; set; }
        public IUserRepository User { get; set; }

        public UnitOfWork(DefaultDataContext context, IMapper mapper)
        {
            _context = context;
            Location = new LocationRepository(_context);
            User = new UserRepository(_context, mapper);
       }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}