using Newtonsoft.Json;

namespace CarSale.Models
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MessageDetail { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Total { get; set; }
    }
}
