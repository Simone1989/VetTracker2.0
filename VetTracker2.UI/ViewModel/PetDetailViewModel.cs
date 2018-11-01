using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetTracker2.Model;
using VetTracker2.UI.Data;

namespace VetTracker2.UI.ViewModel
{
    public class PetDetailViewModel : ViewModelBase, IPetDetailViewModel
    {
        private IPetDataService _dataService;

        public PetDetailViewModel(IPetDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task LoadAsync(int petId)
        {
            Pet = await _dataService.GetByIdAsync(petId);
        }

        private Pet _pet;

        public Pet Pet
        {
            get { return _pet; }
            private set
            {
                _pet = value;
                OnPropertyChanged();
            }
        }

    }
}
