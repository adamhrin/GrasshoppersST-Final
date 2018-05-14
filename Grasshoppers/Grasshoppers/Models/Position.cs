using Newtonsoft.Json;
using Xamarin.Forms;

namespace Grasshoppers.Models
{
    public class Position : Component
    {
        private Color _isRegisteredPlayerColor = Color.FromHex(Application.Current.Resources["RegisteredBrigadeColorString"].ToString());
        private Color _neutralColor = Color.FromHex(Application.Current.Resources["PageBackgroundColorString"].ToString());

        public Position()
        {
            NumberOfHours = 3;
        }

        [JsonProperty("numberOfHours")]
        public int NumberOfHours { get; set; }

        [JsonIgnore]
        public string NumberOfHoursString
        {
            get { return "Hodín: " + NumberOfHours; }
        }
        
        [JsonIgnore]
        public string NameNumberOfHours
        {
            get
            {
                return Name + " (" + NumberOfHours + ")";
            }
        }

        [JsonProperty("registeredPlayer")]
        public Player RegisteredPlayer { get; set; }
        [JsonIgnore]
        public bool IsChosenForBrigade { get; set; }

        //hovori ci ja som prihlaseny na tuto poziciu
        [JsonProperty("isRegisteredPlayerForPosition")]
        public bool IsRegisteredPlayerForPosition { get; set; }

        //hovori ci ja som prihlaseny na niektoru poziciu v brigade ktorej sucastou je tato pozicia
        [JsonProperty("isRegisteredPlayerForBrigade")]
        public bool IsRegisteredPlayerForBrigade { get; set; }

        [JsonIgnore]
        public Color IsRegisteredPlayerColor
        {
            get
            {
                if (IsRegisteredPlayerForPosition)
                {
                    return _isRegisteredPlayerColor;
                }
                return _neutralColor;
            }
        }

        [JsonIgnore]
        public bool IsRegisterBtnVisible
        {
            get
            {
                if (!IsRegisteredPlayerForBrigade)
                {
                    return RegisteredPlayer == null;
                }
                return false;
            }
        }

        [JsonIgnore]
        public bool IsUnregisterBtnVisible
        {
            get
            {
                if (IsRegisteredPlayerForBrigade)
                {
                    return IsRegisteredPlayerForPosition;
                }
                return false;
            }
        }
    }
}
