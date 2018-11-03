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
using VetTracker2.UI.Data.Repositories;
using VetTracker2.UI.Event;
using VetTracker2.UI.Wrapper;

namespace VetTracker2.UI.ViewModel
{
    public class PetDetailViewModel : ViewModelBase, IPetDetailViewModel
    {
        private IPetRepository _petRepository;
        private IEventAggregator _eventAggregator;
        private PetWrapper _pet;
        private bool _hasChanges;

        public PetDetailViewModel(IPetRepository petRepository, IEventAggregator eventAggregator)
        {
            _petRepository = petRepository;
            _eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public async Task LoadAsync(int petId)
        {
            var pet = await _petRepository.GetByIdAsync(petId);

            Pet = new PetWrapper(pet);
            Pet.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _petRepository.HasChanges();
                }
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

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }

        private async void OnSaveExecute()
        {
            await _petRepository.SaveAsync();
            HasChanges = _petRepository.HasChanges();
            _eventAggregator.GetEvent<AfterPetSavedEvent>().Publish(
                new AfterPetSavedEventArgs
                {
                    Id = Pet.Id,
                    DisplayMember = $"{Pet.Name} the {Pet.Type}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Pet != null && !Pet.HasErrors && HasChanges;
        }
    }
}
