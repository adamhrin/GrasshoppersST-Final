using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Grasshoppers.Models
{
    public class Brigade : Event
    {
        private Color _registeredColor = Color.FromHex(Application.Current.Resources["RegisteredBrigadeColorString"].ToString());
        private Color _neutralColor = Color.FromHex(Application.Current.Resources["BrigadeColorString"].ToString());


        public Brigade() : base()
        {
            //DateTraining = DateTime.Now;
            StartTime = TimeSpan.FromHours(12);
            EndTime = StartTime.Add(TimeSpan.FromHours(3));
            StartDate = DateTime.Now.Date + StartTime;
            EndDate = DateTime.Now.Date + EndTime;
            LeadBorderColor = _neutralColor;//farba sa bude menit na zaklade vyjadrenia hraca
            Color = _neutralColor;//farba sa bude menit na zaklade vyjadrenia hraca
        }

        [JsonProperty("idBrigade")]
        public int Id { get; set; }

        private Location _location;
        [JsonProperty("location")]
        public Location Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (value != null)
                {
                    _location = value;
                    Title = "Brigáda: " + _location.Name;
                }
            }
        }

        private bool _isRegisteredPlayer;
        [JsonProperty("isRegisteredPlayer")]
        public bool IsRegisteredPlayer
        {
            get
            {
                return _isRegisteredPlayer;
            }
            set
            {
                _isRegisteredPlayer = value;
                if (value)
                {
                    LeadBorderColor = this._registeredColor;
                    Color = this._registeredColor;
                }
                else
                {
                    LeadBorderColor = this._neutralColor;
                    Color = this._neutralColor;
                }
            }
        }

        [JsonProperty("positions")]
        public List<Position> Positions { get; set; }

        private League _league;
        [JsonProperty("league")]
        public League League
        {
            get
            {
                return _league;
            }
            set
            {
                if (value != null)
                {
                    _league = value;
                }
            }
        }
    }
}
