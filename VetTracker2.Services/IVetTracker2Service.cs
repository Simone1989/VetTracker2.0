using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using VetTracker2.Model;

namespace VetTracker2.Services
{
    [ServiceContract]
    public interface IVetTracker2Service
    {
        [OperationContract]
        Task<Pet> GetByIdAsync(int petId);

        [OperationContract]
        Task SaveAsync();

        [OperationContract]
        bool HasChanges();

        [OperationContract]
        void Add(Pet pet);

        [OperationContract]
        void Delete(Pet model);
    }
}
