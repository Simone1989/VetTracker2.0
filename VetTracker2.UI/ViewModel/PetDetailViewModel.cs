using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VetTracker2.Model;
using VetTracker2.UI.Data;
using VetTracker2.UI.Event;

namespace VetTracker2.UI.ViewModel
{
    public class PetDetailViewModel : ViewModelBase, IPetDetailViewModel
    {
        private IPetDataService _dataService;
        private IEventAggregator _eventAggregator;

        public PetDetailViewModel(IPetDataService dataService, IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenPetDetailViewEvent>().Subscribe(OnOpenPetDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private async void OnSaveExecute()
        {
            await _dataService.SaveAsync(Pet);
            _eventAggregator.GetEvent<AfterPetSavedEvent>().Publish(
                new AfterPetSavedEventArgs
                {
                    Id = Pet.Id,
                    DisplayMember = $"{Pet.Name} the {Pet.Type}"
                });
        }

        private bool OnSaveCanExecute()
        {
            // Validate
            return true;
        }

        private async void OnOpenPetDetailView(int petId)
        {
            await LoadAsync(petId);
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

        public ICommand SaveCommand { get; }
    }
}
