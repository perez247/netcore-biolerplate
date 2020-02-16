using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Persistence.Repository;

namespace Persistence.Triggers
{
    public class UserTriggers
    {
        /// <summary>
        /// When deleting this user delete the following:
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="DataContext"></param>
        /// <returns>Void</returns>
        public static async Task OnDeletingUser(IDeletingEntry<User, DbContext> entity, DefaultDataContext DataContext) {
            
           // Delete Address of user
            var address = await DataContext.Addresses.SingleOrDefaultAsync(c => c.Type == nameof(User) && c.TypeId == entity.Entity.Id);
                if(address != null)
                    DataContext.Addresses.Remove(address);

            // Delete Contact of user
            var contact = await DataContext.Contacts.SingleOrDefaultAsync(c => c.Type == nameof(User) && c.TypeId == entity.Entity.Id);
                if(contact != null)
                    DataContext.Contacts.Remove(contact);

            // Delete collections of user that starts its own deleting
            var collections = await DataContext.Collections.SingleOrDefaultAsync(c => c.Type == nameof(User) && c.TypeId == entity.Entity.Id);
                if(collections != null)
                    DataContext.Collections.Remove(collections);

        }
    }
}