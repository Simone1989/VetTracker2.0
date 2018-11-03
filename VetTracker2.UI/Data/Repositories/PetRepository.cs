using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VetTracker2.DataAccess;
using VetTracker2.Model;

namespace VetTracker2.UI.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly VetTrackerContext _context;

        public PetRepository(VetTrackerContext context)
        {
            _context = context;
        }
        public async Task<Pet> GetByIdAsync(int petId)
        {
            return await _context.Pets.SingleAsync(p => p.Id == petId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
