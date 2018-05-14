using Grasshoppers.Initializers;
using Grasshoppers.Models;
using Grasshoppers.ViewModels;
using Grasshoppers.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grasshoppers.Factories
{
    public static class ViewsFactory
    {
        public async static Task CreateViewFromEvent(object tappedEvent, EventsViewModel eventsViewModel, INavigation navigation)
        {
            if (tappedEvent is Training)
            {
                var trainingsViewModel = new TrainingsViewModel();
                trainingsViewModel.SelectedTraining = tappedEvent as Training;
                await navigation.PushAsync(new TrainingPage(trainingsViewModel)); // for user
            }
            else if (tappedEvent is Brigade)
            {
                var brigadesViewModel = new BrigadesViewModel();
                brigadesViewModel.SelectedBrigade = tappedEvent as Brigade;
                await navigation.PushAsync(new BrigadePage(brigadesViewModel));
            }
            else if (tappedEvent is Match)
            {
                await Connectivity.DoIfConnectedAndReachable(async () =>
                {
                    var matchesViewModel = new MatchesViewModel();
                    matchesViewModel.SelectedMatch = tappedEvent as Match;
                    await navigation.PushAsync(new MatchPage(matchesViewModel));
                });
            }
        }
    }

    
}
