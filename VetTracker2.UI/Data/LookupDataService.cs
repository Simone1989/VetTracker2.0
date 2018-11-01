using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetTracker2.DataAccess;
using VetTracker2.Model;

namespace VetTracker2.UI.Data
{
    public class LookupDataService : IPetLookupDataService
    {
        private readonly Func<VetTrackerContext> _contextCreator;

        public LookupDataService(Func<VetTrackerContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetPetLookupAsync()
        {
            using(var context = _contextCreator())
            {
                return await context.Pets.AsNoTracking().Select(pet =>
                new LookupItem
                {
                    Id = pet.Id,
                    DisplayMember = pet.Name + " the " + pet.Type
                })
                .ToListAsync();
            }
        }
    }
}
