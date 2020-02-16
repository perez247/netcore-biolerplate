using System;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        ILocationRepository Location { get; set; }
        IUserRepository User { get; set; }
        Task<bool> Complete();
    }
}