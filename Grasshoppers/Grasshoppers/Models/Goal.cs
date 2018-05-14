using Grasshoppers.Helpers;
using Newtonsoft.Json;
using System;

namespace Grasshoppers.Models
{
    public class Goal
    {
        public Goal()
        {
            Period = 1;
            Time = new TimeSpan();
        }

        [JsonIgnore]
        public bool IsAdmin => Settings.AdminLevel == 1;
        
        [JsonProperty("idGoal")]
        public int Id { get; set; }
        [JsonProperty("scorer")]
        public Player Scorer { get; set; }
        [JsonProperty("assistent")]
        public Player Assistent { get; set; }

        [JsonIgnore]
        public TimeSpan Time
        {
            get;
            set;
        }

        [JsonProperty("timeString")]
        public string TimeStringInit
        {
            get
            {
                return TimeString;
            }
            set
            {
                if (value != null || value != "")
                {
                    Time = TimeSpan.Parse(value);
                }
            }
        }

        [JsonProperty("period")]
        public int Period { get; set; }
        [JsonIgnore]
        public string TimeString
        {
            get { return string.Format("{0:mm\\:ss}", Time); }
        }

        [JsonProperty("opponentScorer")]
        public string OpponentScorer { get; set; }

        [JsonIgnore]
        public string OpponentScorerString
        {
            get { return "G: " + OpponentScorer; }
        }

        [JsonProperty("opponentAssistent")]
        public string OpponentAssistent { get; set; }

        [JsonIgnore]
        public string OpponentAssistentString
        {
            get { return "A: " + OpponentAssistent; }
        }
    }
}
