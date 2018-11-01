using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VetTracker2.Model;
using VetTracker2.UI.Data;

namespace VetTracker2.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationViewModel navigationViewModel, IPetDetailViewModel petDetailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            PetDetailViewModel = petDetailViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public INavigationViewModel NavigationViewModel { get; }
        public IPetDetailViewModel PetDetailViewModel { get; }
    }
}
