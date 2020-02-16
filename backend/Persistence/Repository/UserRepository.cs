using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Infrastructure.CommonDataStructure.Address;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        public DefaultDataContext _dataContext { get; set; }
        public IMapper _mapper { get; set; }

        public UserRepository(DefaultDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }


        public async Task<UserDetail> GetUserAboutMe(Guid UserId) {
            var userDetails = await _dataContext.UserDetails.SingleOrDefaultAsync(u => u.UserId == UserId);

            if(userDetails == null)
                throw new NotFoundException(nameof(User),"unknown");

            userDetails.Contact = await _dataContext.Contacts.SingleOrDefaultAsync(c => c.TypeId == UserId);

            // Get Global Address
            userDetails.Address = await _dataContext.Addresses
                                        .Include(a => a.Country)
                                        .Include(a => a.State)
                                        .FirstOrDefaultAsync(a => a.TypeId == UserId && a.Global);

            // Get Local Address
            userDetails.LocalAddress = await _dataContext.Addresses
                                        .Include(a => a.Country)
                                        .Include(a => a.State)
                                        .FirstOrDefaultAsync(a => a.TypeId == UserId && !a.Global);

            return userDetails;
        }
    }
}