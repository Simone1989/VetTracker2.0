
using Autofac;
using Prism.Events;
using VetTracker2.DataAccess;
using VetTracker2.UI.Data;
using VetTracker2.UI.ViewModel;

namespace VetTracker2.UI.Startup
{
    public class Bootstrapper
    {
        // Bootstrapper class is responsible for creating the Autofac container

        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<VetTrackerContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<PetDetailViewModel>().As<IPetDetailViewModel>();

            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            // Whenever a IPetDataService is required somewhere, it will create an instance of the PetDataService class
            builder.RegisterType<PetDataService>().As<IPetDataService>();

            return builder.Build();
        }
    }
}
