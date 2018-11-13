using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VetTracker2.DataAccess;
using VetTracker2.Model;

namespace VetTracker2.Services
{
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.PerCall)]
    public class VetTracker2Service : IVetTracker2Service
    {
        private readonly VetTrackerContext _context;

        public VetTracker2Service(VetTrackerContext context)
        {
            _context = context;
        }

        public void Add(Pet pet)
        {
            _context.Pets.Add(pet);
        }

        public void Delete(Pet model)
        {
            _context.Pets.Remove(model);
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
