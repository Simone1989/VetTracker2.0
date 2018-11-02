using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VetTracker2.DataAccess;
using VetTracker2.Model;

namespace VetTracker2.UI.Data
{
    public class PetDataService : IPetDataService
    {
        private readonly Func<VetTrackerContext> _contextCreator;

        public PetDataService(Func<VetTrackerContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<Pet> GetByIdAsync(int petId)
        {
            using(var context = _contextCreator())
            {
                return await context.Pets.AsNoTracking().SingleAsync(p => p.Id == petId);
            }
        }

        public async Task SaveAsync(Pet pet)
        {
            using (var context = _contextCreator())
            {
                context.Pets.Attach(pet);
                context.Entry(pet).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
