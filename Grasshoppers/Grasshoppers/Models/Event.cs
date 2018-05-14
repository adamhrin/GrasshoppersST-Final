using Newtonsoft.Json;
using System;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;

namespace Grasshoppers.Models
{
    public class Event : IAppointment
    {
        private const string timeFormat = "HH:mm";
        private const string dateFormat = "dddd dd.MM.yyyy";
        public Event()
        {
            this.ItemBackgroundColor = Color.White;
        }

        //[JsonConverter(typeof(UnixDateTimeConverter))]
        //[JsonProperty("startDate")]
        [JsonIgnore]
        public DateTime StartDate { get; set; }

        [JsonProperty("startDateTimeString")]
        public string StartDateInitString
        {
            get
            {
                return StartDate.ToString("dd.MM.yyyy HH:mm");
            }
            set
            {
                if (value != null || value != "")
                {
                    StartDate = DateTime.Parse(value);
                }
            }
        }

        //[JsonConverter(typeof(UnixDateTimeConverter))]
        //[JsonProperty("endDate")]
        [JsonIgnore]
        public DateTime EndDate { get; set; }

        [JsonProperty("endDateTimeString")]
        public string EndDateTimeInitString
        {
            get
            {
                return EndDate.ToString("dd.MM.yyyy HH:mm");
            }
            set
            {
                if (value != null || value != "")
                {
                    EndDate = DateTime.Parse(value);
                }
            }
        }

        [JsonIgnore]
        public string Title { get; set; }
        [JsonIgnore]
        public Color Color { get; set; }
        [JsonIgnore]
        public string Detail { get; set; }
        [JsonIgnore]
        public Color ItemBackgroundColor { get; set; }
        [JsonIgnore]
        public Color LeadBorderColor { get; set; }
        [JsonIgnore]
        public bool IsAllDay { get; set; }
        [JsonIgnore]
        public TimeSpan StartTime { get; set; }

        [JsonProperty("startTimeString")]
        public string StartTimeInitString
        {
            set
            {
                if (value != null || value != "")
                {
                    StartTime = TimeSpan.Parse(value);
                }
            }
        }
        [JsonIgnore]
        public TimeSpan EndTime { get; set; }
        
        [JsonProperty("endTimeString")]
        public string EndTimeInitString
        {
            set
            {
                if (value != null || value != "")
                {
                    EndTime = TimeSpan.Parse(value);
                }
            }
        }
        [JsonIgnore]
        public string StartDateString
        {
            get
            {
                return this.StartDate.ToString(dateFormat);
            }
        }
        [JsonIgnore]
        public string StartTimeString
        {
            get
            {
                return this.StartDate.ToString(timeFormat);
            }
        }
        [JsonIgnore]
        public string EndTimeString
        {
            get
            {
                return this.EndDate.ToString(timeFormat);
            }
        }
        [JsonIgnore]
        public string FromToString
        {
            get
            {
                return this.StartTimeString + " - " + this.EndTimeString;
            }
        }
    }
}
