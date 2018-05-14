using Newtonsoft.Json;

namespace Grasshoppers.Models
{
    public class Component
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
