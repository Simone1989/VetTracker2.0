using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VetTracker2.Model;
using VetTracker2.UI.Data;
using VetTracker2.UI.Event;

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
            Pets = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _petLookupService.GetPetLookupAsync();
            Pets.Clear();
            foreach (var item in lookup)
            {
                Pets.Add(item);
            }
        }

        public ObservableCollection<LookupItem> Pets { get; }

        private LookupItem _selectedPet;

        public LookupItem SelectedPet
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
