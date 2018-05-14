using Grasshoppers.Extensions;
using Grasshoppers.Models;
using Grasshoppers.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grasshoppers.ViewModels
{
    public class EventsViewModel : INotifyPropertyChanged
    {
        private DateTime? selectedDate;
        private Event _selectedEvent = new Event();
        private ObservableCollection<Event> _events = new ObservableCollection<Event>();

        public EventsViewModel()
        {
            _isBusy = false;
            this.SelectedEvents = new ObservableCollection<Event>();
            this.selectedDate = DateTime.Today;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value) // only used when value is changed, not when just set
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        /**
         * tu sa budu ziskavat vsetky udalosti na vybrany mesiac (alebo ak si prisposobim tak napriklad
         * na tento mesiac + mesiac dozadu + mesiac dopredu
         * a to znamena vsetky treningy, zapasy, brigady a informacie
         * -- ZATIAL VSETKY
         */
        public async Task InitializeEvents()
        {
            var trainingsServices = new TrainingsServices();
            var brigadesServices = new BrigadesServices();
            var matchesServices = new MatchesServices();

            var eventsHelp = new ObservableCollection<Event>();
            IsBusy = true;

            //if (this.Events == null || (this.Events != null && !this.Events.Any()))
            //{
            eventsHelp.AddRange(await trainingsServices.GetTrainingsForMonthAsync(DateTime.Now));
            eventsHelp.AddRange(await brigadesServices.GetBrigadesForMonth(DateTime.Now));
            eventsHelp.AddRange(await matchesServices.GetMatchesForMonth(DateTime.Now));
            //}
            
            if (eventsHelp.Count > 0) // akutalizujem eventy iba ak som nejaky vytiahol z db
            {
                this.Events = null;
                this.Events = new ObservableCollection<Event>();
                this.Events = eventsHelp;
            }
            IsBusy = false;
            this.UpdateSelectedEvents(this.selectedDate);
        }

        public string DayLabel
        {
            get
            {
                if (this.SelectedDate.HasValue)
                {
                    var date = SelectedDate.Value;
                    var result = string.Format("{0}, {1} {2}", date.DayOfWeek, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month), date.Day);

                    if (Device.RuntimePlatform == Device.Android)
                    {
                        result = result.ToUpper();
                    }

                    return result;
                }

                return null;
            }
        }


        public ObservableCollection<Event> Events
        {
            get { return this._events; }
            set
            {
                if (value != this._events)
                {
                    this._events = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public DateTime? SelectedDate
        {
            get
            {
                return this.selectedDate;
            }
            set
            {
                if (value != this.selectedDate)
                {
                    this.selectedDate = value;
                    this.OnPropertyChanged();
                    this.UpdateSelectedEvents(value);
                }
            }
        }
        
        public ObservableCollection<Event> SelectedEvents { get; }

        private void UpdateSelectedEvents(DateTime? value)
        {
            this.SelectedEvents.Clear();
            foreach (var item in this.Events)
            {
                if (item.StartDate.CompareTo(value.Value) >= 0 && item.StartDate.CompareTo(value.Value.AddDays(1)) < 0)
                {
                    //this.SelectedEvents.Add(new Event(item.StartDate, item.EndDate, item.Title, item.LeadBorderColor, item.ItemBackgroundColor));
                    this.SelectedEvents.Add(item);
                }
            }
            this.OnPropertyChanged("DayLabel");
        }

        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                if (_selectedEvent != value) // only used when value is changed, not when just set
                {
                    _selectedEvent = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
