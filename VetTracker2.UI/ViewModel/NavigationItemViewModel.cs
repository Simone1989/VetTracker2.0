using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VetTracker2.UI.Event;

namespace VetTracker2.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayMember;
        private IEventAggregator _eventAggregator;

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            OpenPetDetailViewCommand = new DelegateCommand(OnOpenPetDetailView);
        }

        public int Id { get;}

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenPetDetailViewCommand { get; }

        private void OnOpenPetDetailView()
        {
            _eventAggregator.GetEvent<OpenPetDetailViewEvent>().Publish(Id);
        }
    }
}
