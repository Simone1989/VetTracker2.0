using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VetTracker2.Model;
using VetTracker2.UI.Data;

namespace VetTracker2.UI.ViewModel
{
    public class NavigationViewModel : INavigationViewModel
    {
        private IPetLookupDataService _petLookupService;

        public NavigationViewModel(IPetLookupDataService petLookupService)
        {
            _petLookupService = petLookupService;
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
    }
}
