using Grasshoppers.Enums;
using Grasshoppers.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Grasshoppers.Models
{
    public class Player
    {
        private Color _acceptColor = Color.FromHex(Application.Current.Resources["AcceptGreenColorString"].ToString());
        private Color _declineColor = Color.FromHex(Application.Current.Resources["DeclineRedColorString"].ToString());

        [JsonProperty("idPlayer")]
        public int Id { get; set; }
        
        [JsonIgnore]
        public string Name
        {
            get { return Firstname + " " + Nick + " " +  Surname; }
        }

        [JsonProperty("nick")]
        public string Nick { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonIgnore]
        public string ConfirmPassword { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("adminLevel")]
        public int AdminLevel { get; set; }

        [JsonProperty("numOfBrigadeHours")]
        public int NumOfBrigadeHours { get; set; }

        [JsonIgnore]
        public string NumOfBrigadeHoursString { get { return "Odrobené brig. hodiny: " + NumOfBrigadeHours; } }

        //public string NumberString
        //{
        //    get
        //    {
        //        if (Number != 0)
        //        {
        //            return Number.ToString();
        //        }
        //        return "";

        //    }
        //    set { int.TryParse(value, Number);}
        //}

        [JsonIgnore]
        public bool IsAdmin
        {
            get { return AdminLevel == 1; }
            set
            {
                if (value)
                {
                    AdminLevel = 1;
                }
                else
                {
                    AdminLevel = 0;
                }
            }
        }

        [JsonIgnore]
        public bool IsSuperAdmin
        {
            get
            {
                return Id != 1; // zatial bulharska konstanta, 1 som ja, vsetci okrem mna maju viditelny switch admina
            }
        }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }
        
        [JsonProperty("playerAcceptedTraining")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AcceptsTrainingOptions AcceptedTraining { get; set; }

        [JsonIgnore]
        public string NumberName {
            get
            {
                return Number + " - " + Name;
            }
        }

        [JsonIgnore]
        public string GoalPlayerString
        {
            get { return "G: " + Name; }
        }

        [JsonIgnore]
        public string AssistentPlayerString
        {
            get { return "A: " + Name; }
        }

        [JsonIgnore]
        public Color AcceptsColor
        {
            get
            {
                if (AcceptedTraining == AcceptsTrainingOptions.Accepted)
                {
                    return AcceptColor;
                }
                else if (AcceptedTraining == AcceptsTrainingOptions.Declined)
                {
                    return _declineColor;
                }
                return Color.White;
            }
        }
        [JsonIgnore]
        public string AcceptsIconIOSString
        {
            get
            {
                if (AcceptedTraining == AcceptsTrainingOptions.Accepted)
                {
                    return "ic_thumb_up.png";
                }
                else if (AcceptedTraining == AcceptsTrainingOptions.Declined)
                {
                    return "ic_thumb_down.png";
                }
                return "ic_help.png";
            }
        }
        [JsonIgnore]
        public string AcceptsIconAndroidString
        {
            get
            {
                if (AcceptedTraining == AcceptsTrainingOptions.Accepted)
                {
                    return "ic_thumb_up_black_24dp.png";
                }
                else if (AcceptedTraining == AcceptsTrainingOptions.Declined)
                {
                    return "ic_thumb_down_black_24dp.png";
                }
                return "ic_help_black_24dp.png";
            }
        }
        [JsonIgnore]
        public string AcceptsIconWindowsString
        {
            get
            {
                if (AcceptedTraining == AcceptsTrainingOptions.Accepted)
                {
                    return "Images/ic_thumb_up.png";
                }
                else if (AcceptedTraining == AcceptsTrainingOptions.Declined)
                {
                    return "Images/ic_thumb_down.png";
                }
                return "Images/ic_help.png";
            }
        }
        [JsonIgnore]
        public Color AcceptColor { get => _acceptColor; set => _acceptColor = value; }
    }
}
