using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VetTracker2.Model;
using VetTracker2.UI.Data;
using VetTracker2.UI.Event;
using System.Linq;

namespace VetTracker2.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IPetLookupDataService _petLookupService;
        private IEventAggregator _eventAggregator;

        public NavigationViewModel(IPetLookupDataService petLookupService, IEventAggregator eventAggregator)
        {
            _petLookupService = petLookupService;
            _eventAggregator = eventAggregator;
            Pets = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterPetSavedEvent>().Subscribe(AfterPetSaved);
        }

        private void AfterPetSaved(AfterPetSavedEventArgs obj)
        {
            var lookupItem = Pets.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _petLookupService.GetPetLookupAsync();
            Pets.Clear();
            foreach (var item in lookup)
            {
                Pets.Add(new NavigationItemViewModel(item.Id, item.DisplayMember));
            }
        }

        public ObservableCollection<NavigationItemViewModel> Pets { get; }

        private NavigationItemViewModel _selectedPet;

        public NavigationItemViewModel SelectedPet
        {
            get { return _selectedPet; }
            set
            {
                _selectedPet = value;
                OnPropertyChanged();
                if (_selectedPet != null)
                {
                    _eventAggregator.GetEvent<OpenPetDetailViewEvent>().Publish(_selectedPet.Id);
                }
            }
        }
    }
}
