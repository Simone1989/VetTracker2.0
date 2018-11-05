using System.Collections.Generic;
using System.Threading.Tasks;
using VetTracker2.Model;

namespace VetTracker2.UI.Data.Repositories
{
    public interface IPetRepository
    {
        Task<Pet> GetByIdAsync(int petId);
        Task SaveAsync();
        bool HasChanges();
        void Add(Pet pet);
        void Delete(Pet model);
    }
}