using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VetTracker2.Model;
using VetTracker2.UI.Data;
using VetTracker2.UI.Event;
using System.Linq;
using VetTracker2.UI.Data.Lookups;

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
            _eventAggregator.GetEvent<AfterPetDeletedEvent>().Subscribe(AfterPetDeleted);
        }

        public ObservableCollection<NavigationItemViewModel> Pets { get; }

        public async Task LoadAsync()
        {
            var lookup = await _petLookupService.GetPetLookupAsync();
            Pets.Clear();
            foreach (var item in lookup)
            {
                Pets.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

        private void AfterPetDeleted(int petId)
        {
            var pet = Pets.SingleOrDefault(p => p.Id == petId);
            if(pet != null)
            {
                Pets.Remove(pet);
            }
        }

        private void AfterPetSaved(AfterPetSavedEventArgs obj)
        {
            var lookupItem = Pets.SingleOrDefault(l => l.Id == obj.Id);
            if (lookupItem == null)
            {
                Pets.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = obj.DisplayMember;
            }
        }
    }
}
