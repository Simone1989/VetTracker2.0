using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VetTracker2.Model;
using VetTracker2.UI.Data;

namespace VetTracker2.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IPetDataService _petDataService;
        private Pet _selectedPet;

        public MainViewModel(IPetDataService petDataService)
        {
            Pets = new ObservableCollection<Pet>();
            _petDataService = petDataService;
        }

        public async Task LoadAsync()
        {
            var pets = await _petDataService.GetAllAsync();

            Pets.Clear();
            foreach (var pet in pets)
            {
                Pets.Add(pet);
            }
        }

        public ObservableCollection<Pet> Pets { get; set; }

        public Pet SelectedPet
        {
            get { return _selectedPet; }
            set
            {
                _selectedPet = value;
                OnPropertyChanged();
            }
        }
    }
}
