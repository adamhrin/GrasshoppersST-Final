
using Newtonsoft.Json;

namespace Grasshoppers.Models
{
    public class Category : Component
    {
        [JsonIgnore]
        public bool IsChosenForTraining { get; set; }
        [JsonIgnore]
        public bool IsChosenForPlayer { get; set; }
    }
}
