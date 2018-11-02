using Prism.Events;

namespace VetTracker2.UI.Event
{
    public class AfterPetSavedEvent : PubSubEvent<AfterPetSavedEventArgs>
    {
    }

    public class AfterPetSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}
