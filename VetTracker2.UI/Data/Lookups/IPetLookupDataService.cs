using System.Collections.Generic;
using System.Threading.Tasks;
using VetTracker2.Model;

namespace VetTracker2.UI.Data.Lookups
{
    public interface IPetLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetPetLookupAsync();
    }
}