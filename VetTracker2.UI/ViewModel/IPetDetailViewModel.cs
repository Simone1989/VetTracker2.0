using System.Threading.Tasks;

namespace VetTracker2.UI.ViewModel
{
    public interface IPetDetailViewModel
    {
        Task LoadAsync(int petId);
    }
}