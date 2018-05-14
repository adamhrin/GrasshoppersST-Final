using Grasshoppers.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Grasshoppers.Models
{
    public class Info : INotifyPropertyChanged
    {
        private const string timeFormat = "HH:mm";
        private const string dateFormat = "dddd dd.MM.yyyy";
        private Color _leadBorderColor = Color.FromHex(Application.Current.Resources["InfoColorString"].ToString());

        [JsonIgnore]
        public bool IsAdmin => Settings.AdminLevel == 1;

        [JsonProperty("idInfo")]
        public int Id { get; set; }

        [JsonProperty("creator")]
        public Player Creator { get; set; }

        [JsonIgnore]
        public DateTime CreationDateTime { get; set; }
        [JsonProperty("creationDateTimeString")]
        public string CreationDateTimeInitString
        {
            set
            {
                if (value != null || value != "")
                {
                    CreationDateTime = DateTime.Parse(value);
                }
            }
        }

        [JsonIgnore]
        public DateTime LastUpdateDateTime { get; set; }
        [JsonProperty("lastUpdateDateTimeString")]
        public string LastUpdateDateTimeInitString
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    WasUpdated = false;
                }
                else
                {
                    LastUpdateDateTime = DateTime.Parse(value);
                    WasUpdated = true;
                }
            }
        }

        [JsonIgnore]
        public bool WasUpdated { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }

        [JsonIgnore]
        public Color LeadBorderColor
        {
            get { return _leadBorderColor; }
        }

        [JsonIgnore]
        public string CreationDateTimeString
        {
            get { return "vytvorené: " + CreationDateTime.ToString(dateFormat) + " " + CreationDateTime.ToString(timeFormat); }
        }

        [JsonIgnore]
        public string LastUpdateDateTimeString
        {
            get { return "upravené: " + LastUpdateDateTime.ToString(dateFormat) + " " + LastUpdateDateTime.ToString(timeFormat); }
        }

        [JsonIgnore]
        public string ShortContent
        {
            get
            {
                if (Content.Length >= 100)
                {
                    return Content.Substring(0, 100) + "...";
                }
                return Content;
            }
        }

        private bool _isWholeContentVisible;
        [JsonIgnore]
        public bool IsWholeContentVisible
        {
            get { return _isWholeContentVisible; }
            set
            {
                _isWholeContentVisible = value;
                OnPropertyChanged();
                OnPropertyChanged("IsShortContentVisible");
            }
        }

        [JsonIgnore]
        public bool IsShortContentVisible
        {
            get { return !_isWholeContentVisible; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
