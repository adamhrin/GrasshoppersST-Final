using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Grasshoppers.Models
{
    public class Match : Event
    {
        private const int numOfLettersAbbreviation = 7;
        private const string _grassString = "Grass";
        private Color _neutralColor = Color.FromHex(Application.Current.Resources["MatchColorString"].ToString());

        public Match() : base()
        {
            //DateTraining = DateTime.Now;
            StartTime = TimeSpan.FromHours(14);
            EndTime = StartTime.Add(TimeSpan.FromHours(2));
            StartDate = DateTime.Now.Date + StartTime;
            EndDate = DateTime.Now.Date + EndTime;
            LeadBorderColor = _neutralColor;//farba sa bude menit na zaklade vyjadrenia hraca
            Color = _neutralColor;//farba sa bude menit na zaklade vyjadrenia hraca
        }

        [JsonProperty("idMatch")]
        public int Id { get; set; }

        [JsonIgnore]
        public string GrassString
        {
            get { return _grassString; }
        }
        
        [JsonProperty("opponentName")]
        public string OpponentName { get; set; }

        private string _opponentAbbreviation = "";
        [JsonProperty("opponentAbbreviation")]
        public string OpponentAbbreviation
        {
            get
            {
                if (_opponentAbbreviation.Length > numOfLettersAbbreviation)
                {
                    return _opponentAbbreviation.Substring(0, numOfLettersAbbreviation);
                }
                return _opponentAbbreviation;
            }
            set
            {
                if (value != null)
                {
                    _opponentAbbreviation = value;
                    Title = Overview;
                }
            }
        }

        [JsonIgnore]
        public int GrassScore
        {
            get
            {
                if (GrassGoals != null)
                {
                    return GrassGoals.Count();
                }
                return 0;
            }
        }

        [JsonIgnore]
        public int OpponentScore
        {
            get
            {
                if (OpponentGoals != null)
                {
                    return OpponentGoals.Count();
                }
                return 0;
            }
        }

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
                }
            }
        }

        [JsonIgnore]
        public string Overview
        {
            get
            {
                return "Zápas: " + _grassString + "-" + OpponentAbbreviation + " (" + GrassScore + "-" + OpponentScore + ")";
            }
        }

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

        private Category _category;
        [JsonProperty("category")]
        public Category Category
        {
            get
            {
                return _category;
            }
            set
            {
                if (value != null)
                {
                    _category = value;
                }
            }
        }

        private List<Goal> _grassGoals;
        [JsonProperty("grassGoals")]
        public List<Goal> GrassGoals
        {
            get { return _grassGoals; }
            set
            {
                if (value != null)
                {
                    _grassGoals = value;
                    Title = Overview;
                }
            }
        }

        private List<Goal> _opponentGoals;
        [JsonProperty("opponentGoals")]
        public List<Goal> OpponentGoals
        {
            get { return _opponentGoals; }
            set
            {
                if (value != null)
                {
                    _opponentGoals = value;
                    Title = Overview;
                }
            }
        }

    }
}
