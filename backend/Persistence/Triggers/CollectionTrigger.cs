using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Persistence.Repository;

namespace Persistence.Triggers
{
    public class CollectionTrigger
    {
        public static async Task OnDeletingCollection(IDeletingEntry<Collection, DbContext> entity, DefaultDataContext DataContext) 
        {
            var photos = await DataContext.Photos.Where(p => p.Type == nameof(entity.Entity.Type) && p.TypeId == entity.Entity.TypeId).ToListAsync();
                if(photos.Count > 0)
                    DataContext.Photos.RemoveRange(photos);
        }
    }
}