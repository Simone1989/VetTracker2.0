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
using VetTracker2.UI.Wrapper;

namespace VetTracker2.UI.ViewModel
{
    public class PetDetailViewModel : ViewModelBase, IPetDetailViewModel
    {
        private IPetDataService _dataService;
        private IEventAggregator _eventAggregator;
        private PetWrapper _pet;

        public PetDetailViewModel(IPetDataService dataService, IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenPetDetailViewEvent>().Subscribe(OnOpenPetDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public async Task LoadAsync(int petId)
        {
            var pet = await _dataService.GetByIdAsync(petId);

            Pet = new PetWrapper(pet);
            Pet.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Pet.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public PetWrapper Pet
        {
            get { return _pet; }
            private set
            {
                _pet = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        private async void OnSaveExecute()
        {
            await _dataService.SaveAsync(Pet.Model);
            _eventAggregator.GetEvent<AfterPetSavedEvent>().Publish(
                new AfterPetSavedEventArgs
                {
                    Id = Pet.Id,
                    DisplayMember = $"{Pet.Name} the {Pet.Type}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Pet != null && !Pet.HasErrors;
        }

        private async void OnOpenPetDetailView(int petId)
        {
            await LoadAsync(petId);
        }
    }
}
