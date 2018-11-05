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
using VetTracker2.UI.View.Services;
using VetTracker2.UI.Wrapper;

namespace VetTracker2.UI.ViewModel
{
    public class PetDetailViewModel : ViewModelBase, IPetDetailViewModel
    {
        private IPetRepository _petRepository;
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private PetWrapper _pet;
        private bool _hasChanges;

        public PetDetailViewModel(IPetRepository petRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _petRepository = petRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        public async Task LoadAsync(int? petId)
        {
            var pet = petId.HasValue ? await _petRepository.GetByIdAsync(petId.Value) : CreateNewPet();

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
            if (Pet.Id == 0)
            {
                Pet.Name = "";
            }
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

        public ICommand DeleteCommand { get; }

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

        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Are you sure you want to delete the client {Pet.Name}?", "Delete?");
            if (result == MessageDialogResult.OK)
            {
                _petRepository.Delete(Pet.Model);
                await _petRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterPetDeletedEvent>().Publish(Pet.Id);
            }
        }

        private Pet CreateNewPet()
        {
            var pet = new Pet();
            _petRepository.Add(pet);
            return pet;
        }
    }
}
