using Prism.Events;
using System;
using System.Threading.Tasks;
using VetTracker2.UI.Event;
using VetTracker2.UI.View.Services;

namespace VetTracker2.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IPetDetailViewModel> _petDetailViewModelCreator;
        private IMessageDialogService _messageDialogService;
        private IPetDetailViewModel _petDetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IPetDetailViewModel> petDetailViewModelCreator, 
            IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _petDetailViewModelCreator = petDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenPetDetailViewEvent>().Subscribe(OnOpenPetDetailView);

            NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public INavigationViewModel NavigationViewModel { get; }

        public IPetDetailViewModel PetDetailViewModel
        {
            get { return _petDetailViewModel; }
            private set
            {
                _petDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        private async void OnOpenPetDetailView(int petId)
        {
            if(PetDetailViewModel!=null && PetDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You have unsaved changes. Discard?", "Discard changes");
                if(result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            PetDetailViewModel =_petDetailViewModelCreator();
            await PetDetailViewModel.LoadAsync(petId);
        }
    }
}
