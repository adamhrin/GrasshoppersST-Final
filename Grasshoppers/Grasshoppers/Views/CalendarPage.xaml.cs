using Grasshoppers.ViewModels;
using Grasshoppers.Factories;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarPage : ContentPage
	{
		public CalendarPage()
		{
			InitializeComponent();
        }

        public CalendarPage(EventsViewModel evm)
        {
            InitializeComponent();
            BindingContext = evm;
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as EventsViewModel).InitializeEvents();
        }

        private async void EventsListView_ItemTapped(object sender, Telerik.XamarinForms.DataControls.ListView.ItemTapEventArgs e)
        {
           var tappedEvent = EventsListView.SelectedItem;
            if (tappedEvent != null)
            {
                var eventsViewModel = BindingContext as EventsViewModel;

                if (eventsViewModel != null)
                {
                    await ViewsFactory.CreateViewFromEvent(tappedEvent, eventsViewModel, Navigation);
                }
            }
        }

        private async void EventsListView_Refreshing(object sender, EventArgs e)
        {
            await (BindingContext as EventsViewModel).InitializeEvents();
            
            EventsListView.IsRefreshing = false;
        }
    }
}