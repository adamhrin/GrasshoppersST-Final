using Grasshoppers.Converters;
using Grasshoppers.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Grasshoppers.Models
{
    public class Training : Event
    {
        private Color _acceptColor = Color.FromHex(Application.Current.Resources["AcceptGreenColorString"].ToString());
        private Color _declineColor = Color.FromHex(Application.Current.Resources["DeclineRedColorString"].ToString());
        private Color _neutralColor = Color.FromHex(Application.Current.Resources["TrainingColorString"].ToString());

        public Training() : base()
        {
            //DateTraining = DateTime.Now;
            StartTime = TimeSpan.FromHours(18.5);
            EndTime = StartTime.Add(TimeSpan.FromHours(1.5));
            StartDate = DateTime.Now.Date + StartTime;
            EndDate = DateTime.Now.Date + EndTime;
            LeadBorderColor = _neutralColor;//farba sa bude menit na zaklade vyjadrenia hraca
            Color = _neutralColor;//farba sa bude menit na zaklade vyjadrenia hraca
        }

        [JsonProperty("idTraining")]
        public int Id { get; set; }

        private Location _location;
        [JsonProperty("location")]
        public Location Location
        {
            get { return _location; }
            set
            {
                if (value != null)
                {
                    _location = value;
                    Title = "Tréning: " + _location.Name;
                }
            }
        }

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }

        [JsonProperty("invitedPlayers")]
        public List<Player> InvitedPlayers { get; set; }

        private AcceptsTrainingOptions _acceptedByPlayer;
        [JsonProperty("acceptedByPlayer")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AcceptsTrainingOptions AcceptedByPlayer
        {
            get
            {
                return _acceptedByPlayer;
            }
            set
            {
                _acceptedByPlayer = value;
                if (value == AcceptsTrainingOptions.Accepted)
                {
                    LeadBorderColor = this._acceptColor;
                    Color = this._acceptColor;
                }
                else if (value == AcceptsTrainingOptions.Declined)
                {
                    LeadBorderColor = this._declineColor;
                    Color = this._declineColor;
                }
                else
                {
                    LeadBorderColor = this._neutralColor;
                    Color = this._neutralColor;
                }
            }
        }
        [JsonIgnore]
        public Color AcceptColor
        {
            get { return _acceptColor; }
        }
        [JsonIgnore]
        public Color DeclineColor
        {
            get{ return _declineColor; }
        }

        private string _acceptedString;
        [JsonIgnore]
        public string AcceptedString
        {
            get
            {
                if (AcceptedByPlayer == AcceptsTrainingOptions.Accepted)
                {
                    return "Momentálne si prihlásený na tréning";
                }
                else if (AcceptedByPlayer == AcceptsTrainingOptions.Declined)
                {
                    return "Momentálne si odhlásený z tréningu";
                }
                return "Čakáme na tvoje vyjadrenie:";
            }
            set
            {
                _acceptedString = value;
            }
        }
        [JsonIgnore]
        public bool IsAccepted {
            get
            {
                return AcceptedByPlayer == AcceptsTrainingOptions.Accepted || AcceptedByPlayer == AcceptsTrainingOptions.NotStated;
            }
        }
        [JsonIgnore]
        public bool IsDeclined
        {
            get
            {
                return AcceptedByPlayer == AcceptsTrainingOptions.Declined || AcceptedByPlayer == AcceptsTrainingOptions.NotStated;
            }
        }
    }
}
