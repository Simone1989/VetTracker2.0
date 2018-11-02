using System.Collections.Generic;
using System.Threading.Tasks;
using VetTracker2.Model;

namespace VetTracker2.UI.Data
{
    public interface IPetDataService
    {
        Task<Pet> GetByIdAsync(int petId);
        Task SaveAsync(Pet pet);
    }
}